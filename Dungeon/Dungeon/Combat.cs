using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary;
using Common;

namespace Dungeon;

internal enum CombatOptions {
	Attack,
	Block,
	Info,
	Flee
}

public class Combat {

	public static void Fight(Entity enemy) {
		Writer.Clear();
		Writer.Ellipsis("Preparing for battle");

		//=== VARIABLES ===\\
		Player player = Program.player;
		CombatOptions playerChoice, enemyChoice;
		Random rand = new();
		enemy.Damage(rand.NextSingle() * .2f * enemy.Race.MaxHealth);

		//loop for combat
		while(true) {

			//choices
			playerChoice = CombatSelect();
			enemyChoice = (rand.Next(2) == 0) ? CombatOptions.Attack : CombatOptions.Block;
			Writer.Clear();
			Writer.WriteLine($"{($"{player.Name} choose:").ToString().PadRight(20)}	{(Enum.GetName(playerChoice).ToUpper())}");
			Writer.WriteLine($"{($"{enemy.Name} chooses:").ToString().PadRight(20)} {(Enum.GetName(enemyChoice).ToUpper())}\n");

			if     (playerChoice == CombatOptions.Info) {
				Info(player, enemy);
				continue;
			}
			else if(playerChoice == CombatOptions.Flee) { 
				if(Flee(player, enemy)) return;
			}
			else if(playerChoice == CombatOptions.Attack) Attack(player, enemy, enemyChoice);
			else if(playerChoice == CombatOptions.Block)  Block (player, enemy, enemyChoice);

			//check result
			if(!player.IsAlive || !enemy.IsAlive) break;

			//display health
			Console.CursorTop = 26;
			Writer.WriteLine($"{($"{player.Name}'s health:").ToString().PadRight(20)}{player.Health}/{player.Race.MaxHealth}");
			Writer.WriteLine($"{( $"{enemy.Name}'s health:").ToString().PadRight(20)}{enemy.Health}/{enemy.Race.MaxHealth}");

		}

		//win/lose conditions
		Console.CursorTop = 25;
		if(player.IsAlive) {
			Writer.WriteLine("\nCongratulations! You won the fight!");
			player.IncrementKillCount();

			Writer.WriteLine($"{player.Name} regained strength and healed {Heal(player, .25f)} HP.");
		}
		else Writer.WriteLine("\nWelp. Your dead.");

		General.WaitForInput();

		Menu.Info();
	}

	internal static CombatOptions CombatSelect() {
		Writer.CursorBottom();

		Writer.WriteLine("What would you like to do?\n");
		for(int i = 0; i <= (int)CombatOptions.Flee; i++)
			Writer.WriteLine($"{i+1}) {(CombatOptions)i}");

		Writer.WriteLine();
		Writer.Write(">> ");
		return (CombatOptions)((int.TryParse(Console.ReadLine().Trim(), out int num) && --num>=0 && num<=(int)CombatOptions.Flee) ? num : (int)CombatOptions.Info);

	}

	internal static void Attack(Entity attacker, Entity defender, CombatOptions defChoice) {

		/*
		Taking race skill level and weapon difficulty into account to determine
			attack accuracy is a good approach. This can be represented as a 
			chance to hit, where the attacker's skill level and weapon 
			difficulty contribute positively and the defender's dodge 
			contributes negatively.
		Once a hit is confirmed, damage calculation should consider the
			attacker's weapon damage and the defender's defense. This could
			involve subtracting the defender's defense from the attacker's
			damage to determine the final damage dealt.
		 */

		switch(defChoice) {
			case CombatOptions.Attack: {
				/*
				if attacker and defender choose attack, defender takes 
				full damage, attacker takes mid damage
				*/

				//calc if atk hit
				//def takes damage - armour
				Writer.WriteLine($"\n{attacker.Name} attacks {defender.Name}...");

				if(DoesHit(attacker, defender)) {
					float dmg = attacker.Weapon.Damage * (1 - defender.Armour.Defense/20);
					dmg = MathF.Round(dmg, 1);
					defender.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {dmg} damage!\n");
				}
				else Writer.WriteLine("...but misses.\n");

				//calc if def hit
				//atk takes half damage - armour
				Writer.WriteLine($"\n{defender.Name} attacks {attacker.Name} back...");

				if(DoesHit(defender, attacker)) {
					float dmg = defender.Weapon.Damage * (1 - attacker.Armour.Defense/20) * .75f;
					dmg = MathF.Round(dmg, 1);
					attacker.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {dmg} damage!\n");
				}
				else Writer.WriteLine("...but misses.\n");


				return;
			}
			case CombatOptions.Block: {
				/*
				if attacker chooses attack, and defender chooses block, 
				defender takes small damage, attacker takes none
				*/

				//calc if atk hit
				//def takes damage - armour
				Writer.WriteLine($"\n{attacker.Name} attacks {defender.Name}...");

				if(DoesHit(attacker, defender)) {
					float dmg = attacker.Weapon.Damage * (1 - defender.Armour.Defense/20) * .6f;
					dmg = MathF.Round(dmg, 1);
					defender.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {MathF.Round(dmg, 1)} damage!\n");

					if(defender.Health <= defender.Race.MaxHealth * .3f)
						Writer.WriteLine($"{defender.Name} regained strength and healed {Heal(defender, .15f)} HP.");
				}
				else {
					Writer.WriteLine($"...but misses.\n");
					if(defender.Health <= defender.Race.MaxHealth * .4f)
						Writer.WriteLine($"\n{defender.Name} regained strength and healed {Heal(defender, .20f)} HP.");
				}

				return;
			}
			default: {
				Writer.WriteLine("ERROR: Invalid defChoice inside attack.");
				return;
			}
		}

	}

	internal static void Block(Entity attacker, Entity defender, CombatOptions defChoice) {
		//todo block

		/*
		To make blocking feel impactful, it could not only increase defense but
			also provide a bonus effect. For example, successful blocks could 
			grant a temporary boost to the attacker's dodge on the next turn, 
			representing the defender's regained footing or tactical advantage.
		Alternatively, successful blocks could grant a small amount of health
			regeneration to both parties, incentivizing strategic defense.
		 */

		switch(defChoice) {
			case CombatOptions.Attack: {
				/*
				if attacker chooses block and defender chooses attack, defender
				takes no damage, attacker takes small damage
				 */
				Writer.WriteLine($"\n{defender.Name} attacks {attacker.Name}...");

				if(DoesHit(defender, attacker)) {
					float dmg = defender.Weapon.Damage * (1 - attacker.Armour.Defense/20) * .2f;
					dmg = MathF.Round(dmg, 1);
					attacker.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {dmg} damage!\n");

					if(attacker.Health <= attacker.Race.MaxHealth * .5f)
						Writer.WriteLine($"{attacker.Name} regained strength and healed {Heal(attacker, .2f)} HP.");
				}
				else {
					Writer.WriteLine($"...but misses.\n");
					if(attacker.Health <= attacker.Race.MaxHealth * .6f)
						Writer.WriteLine($"{attacker.Name} regained strength and healed {Heal(attacker, .25f)} HP.");
					
				}
				

				return;
			}
			case CombatOptions.Block: {
				/*
				if attacker and defender choose block, neither take damage and
				both heal slightly, attacker heals more though
				 */
				Writer.WriteLine();
				Writer.WriteLine($"Both opponents choose to block! The temporary break in the fight allows you to regain some health.\n");

				//heal attacker
				if(attacker.Health <= attacker.Race.MaxHealth * .75f) 
					 Writer.WriteLine($"{attacker.Name} regained {Heal(attacker, .25f)} HP.");
				else Writer.WriteLine($"{attacker.Name} is already quite healthy and doesn't heal.");

				//heal defender
				if(defender.Health <= defender.Race.MaxHealth * .6f) 
					 Writer.WriteLine($"{defender.Name} regained {Heal(defender, .17f)} HP.");
				else Writer.WriteLine($"{defender.Name} is already quite healthy and doesn't heal.");

				return;
			}
			default: {
				Writer.WriteLine("ERROR: Invalid defChoice inside block.");
				return;
			}
		}

	}

	internal static bool DoesHit(Entity attacker, Entity defender) {
		float accuracy = (attacker.Race.Skill / attacker.Weapon.Difficulty);
		accuracy *= 1-(defender.Armour.Dodge / 20);
		if(new Random().NextSingle()*20 <= accuracy*20) return true;
		else return false;
	}

	internal static float Heal(Entity entity, float mod) {
		float hp;
		if(entity.Health * mod >= 10) {
			hp = new Random().NextSingle() * mod * entity.Health;
			hp = Math.Clamp(MathF.Round(hp, 1), 3, entity.Health * mod);
		}
		else hp = new Random().NextSingle()*5 + 3;

		hp = MathF.Round(hp, 1);
		entity.Heal(hp);
		return hp;
	}

	internal static void Info(Player player, Entity enemy) {
		Writer.Clear();
		Writer.CursorTop();
		player.Display();
		enemy.Display();
	}

	internal static bool Flee(Player player, Entity enemy) {
		//todo flee

		/*
		Using the dodge stat to determine the success of running makes sense.
			It could be influenced by factors such as the player's current 
			health (lower health could reduce the chance of successfully
			escaping) and the enemy's actions (running might be easier if the
			enemy is focusing on attacking rather than blocking).
		Taking some damage when attempting to run but failing to dodge an 
			attack adds risk to the action, discouraging players from spamming 
			it as a means of escape.
		Increasing the chance of dodging while attempting to run adds a 
			strategic element, rewarding players who choose to disengage from 
			combat.
		 */

		Writer.WriteLine();
		Writer.WriteLine($"You attempt to run away...");

		float runChance = player.Armour.Dodge / 20;
		runChance *= (player.Health / player.Race.MaxHealth) * .75f + .25f;

		if(runChance >= new Random().NextSingle()) {
			Writer.WriteLine("...and succeed!\n\nCongratulations! You run away.\n");

			if(player.Health <= player.Race.MaxHealth * .75f) {
				Writer.WriteLine("The relief from the fight allows you to regain some health.");
				Writer.WriteLine($"You heal {Heal(player, .25f)} HP.\n");
			}
			
			return true;
		}


		Writer.WriteLine("...but fail, which gives the enemy a chance to attack...");

		if (DoesHit(player, enemy)){
			float dmg = enemy.Weapon.Damage * (1 - player.Armour.Defense/20) * .5f;
			dmg = MathF.Round(dmg, 1);
			player.Damage(dmg);
			Writer.WriteLine($"...and they hit, dealing {MathF.Round(dmg, 1)} damage!");
		}
		else {
			Writer.WriteLine("...but they miss! You take a moment to recover before your next move.");
			if(player.Health <= player.Race.MaxHealth * .75f)
				Writer.WriteLine($"You heal {Heal(player, .25f)} HP.\n");
		}

		return false;

	}

}
