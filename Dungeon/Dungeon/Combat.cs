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

	//=== VARIABLES ===\\
	internal static Player player = Program.player;
	internal static Entity enemy;
	internal static bool isBoss;
	internal static CombatOptions playerChoice, enemyChoice;
	internal static Random rand = new Random();

	//=== METHODS ===\\
	public static void Fight(Entity e) {

		//instantiate variables
		enemy = e;
		isBoss = enemy.Race.Type == RaceType.Dragon;

		//setup
		if(!isBoss) enemy.Damage(rand.NextSingle() * .2f * enemy.Race.MaxHealth);
		Writer.Clear();
		Writer.Ellipsis(isBoss ? "A deep roar trembles the ground" : "Preparing for battle" );
		Console.CursorTop = 26;
		Writer.WriteLine($"{($"{player.Name}'s health:").ToString().PadRight(20)}{player.Health}/{player.Race.MaxHealth}");
		Writer.WriteLine($"{($"{enemy.Name }'s health:").ToString().PadRight(20)}{enemy.Health}/{enemy.Race.MaxHealth}");

		//loop for combat
		while(true) {

			//choices
			playerChoice = CombatSelect();
			enemyChoice = (rand.Next(2) == 0) ? CombatOptions.Attack : CombatOptions.Block;
			Writer.Clear();
			Writer.WriteLine($"{($"{player.Name} chose:" ).ToString().PadRight(20)}	{(Enum.GetName(playerChoice).ToUpper())}");
			Writer.WriteLine($"{($"{enemy.Name} chooses:").ToString().PadRight(20)} {(Enum.GetName(enemyChoice).ToUpper())}\n");

			if     (playerChoice == CombatOptions.Attack) Attack();
			else if(playerChoice == CombatOptions.Block)  Block ();
			else if(playerChoice == CombatOptions.Info) { Info(); continue; }
			else if(playerChoice == CombatOptions.Flee)   if(Flee()) return;

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
			player.IncreaseScore(enemy.Score);

			if(isBoss) {
				General.WaitForInput();
				return;
			}

			Writer.WriteLine($"{player.Name} regained strength and healed {Heal(player, .25f)} HP.");
		}
		else Writer.WriteLine("\nWelp. Your dead.");

		General.WaitForInput();
		Menu.Info();
	}

	internal static CombatOptions CombatSelect() {
		Writer.CursorBottom();

		int optionAmount = (isBoss ? 3 : 4);

		Writer.WriteLine("What would you like to do?\n");
		for(int i = 0; i < optionAmount; i++)
			Writer.WriteLine($"{i+1}) {(CombatOptions)i}");

		Writer.WriteLine();
		Writer.Write(">> ");
		return ((int.TryParse(Console.ReadLine().Trim(), out int num) && --num>=0 && num<optionAmount) 
			? (CombatOptions)num : CombatOptions.Info);
	}

	internal static void Attack() {
		/*
		 * NOTES
		Taking race skill level and weapon difficulty into account to determine
			attack accuracy is a good approach. This can be represented as a 
			chance to hit, where the player's skill level and weapon 
			difficulty contribute positively and the enemy's dodge 
			contributes negatively.
		Once a hit is confirmed, damage calculation should consider the
			player's weapon damage and the enemy's defense. This could
			involve subtracting the enemy's defense from the player's
			damage to determine the final damage dealt.
		 */
		Random random = new();
		
		switch(enemyChoice) {
			case CombatOptions.Attack: {
				/*
				if player and enemy choose attack, enemy takes 
				full damage, player takes mid damage
				*/

				//calc if atk hit
				//def takes damage - armour
				Writer.WriteLine($"\n{player.Name} attacks {enemy.Name}...");

				if(DoesHit(player, enemy)) {
					float dmg = player.Weapon.Damage * (1 - enemy.Armour.Defense/20);
					dmg *= (random.NextSingle() * .25f + .775f);
					dmg = MathF.Round(dmg, 1);
					enemy.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {dmg} damage!\n");
				}
				else Writer.WriteLine("...but misses.\n");

				//calc if def hit
				//atk takes half damage - armour
				Writer.WriteLine($"\n{enemy.Name} attacks {player.Name} back...");

				if(DoesHit(enemy, player)) {
					float dmg = enemy.Weapon.Damage * (1 - player.Armour.Defense/20) * .75f;
					dmg += dmg * (random.NextSingle() * .25f + .125f);
					dmg = MathF.Round(dmg, 1);
					player.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {dmg} damage!\n");
				}
				else Writer.WriteLine("...but misses.\n");


				return;
			}
			case CombatOptions.Block: {
				/*
				if player chooses attack, and enemy chooses block, 
				enemy takes small damage, player takes none
				*/

				//calc if atk hit
				//def takes damage - armour
				Writer.WriteLine($"\n{player.Name} attacks {enemy.Name}...");

				if(DoesHit(player, enemy)) {
					float dmg = player.Weapon.Damage * (1 - enemy.Armour.Defense/20) * .6f;
					dmg += dmg * (random.NextSingle() * .25f + .125f);
					dmg = MathF.Round(dmg, 1);
					enemy.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {MathF.Round(dmg, 1)} damage!\n");

					if(enemy.Health <= enemy.Race.MaxHealth * .3f)
						Writer.WriteLine($"{enemy.Name} regained strength and healed {Heal(enemy, .15f)} HP.");
				}
				else {
					Writer.WriteLine($"...but misses.\n");
					if(enemy.Health <= enemy.Race.MaxHealth * .4f)
						Writer.WriteLine($"\n{enemy.Name} regained strength and healed {Heal(enemy, .2f)} HP.");
				}

				return;
			}
			default: {
				Writer.WriteLine("ERROR: Invalid enemyChoice inside attack.");
				return;
			}
		}

	}

	internal static void Block() {
		/*
		To make blocking feel impactful, it could not only increase defense but
			also provide a bonus effect. For example, successful blocks could 
			grant a temporary boost to the player's dodge on the next turn, 
			representing the enemy's regained footing or tactical advantage.
		Alternatively, successful blocks could grant a small amount of health
			regeneration to both parties, incentivizing strategic defense.
		 */
		Random random = new();


		switch(enemyChoice) {
			case CombatOptions.Attack: {
				/*
				if player chooses block and enemy chooses attack, enemy
				takes no damage, player takes small damage
				 */
				Writer.WriteLine($"\n{enemy.Name} attacks {player.Name}...");

				if(DoesHit(enemy, player)) {
					float dmg = enemy.Weapon.Damage * (1 - player.Armour.Defense/20) * .2f;
					dmg += dmg * (random.NextSingle() * .25f + .125f);
					dmg = MathF.Round(dmg, 1);
					player.Damage(dmg);
					Writer.WriteLine($"...and hits, dealing {dmg} damage!\n");

					if(player.Health <= player.Race.MaxHealth * .5f) 
						Writer.WriteLine($"{player.Name} regained strength and healed {Heal(player, .20f)} HP.");
				}
				else {
					Writer.WriteLine($"...but misses.\n");
					if(player.Health <= player.Race.MaxHealth * .6f) 
						Writer.WriteLine($"{player.Name} regained strength and healed {Heal(player, .25f)} HP.");
				}
				

				return;
			}
			case CombatOptions.Block: {
				/*
				if player and enemy choose block, neither take damage and
				both heal slightly, player heals more though
				 */
				Writer.WriteLine();
				Writer.WriteLine($"Both opponents choose to block! The temporary break in the fight allows you to regain some health.\n");

				//heal player
				if(player.Health <= player.Race.MaxHealth * .75f)
					 Writer.WriteLine($"{player.Name} regained {Heal(player, .25f)} HP.");
				else Writer.WriteLine($"{player.Name} is already quite healthy and doesn't heal.");

				//heal enemy
				if(enemy.Health <= enemy.Race.MaxHealth * .6f) 
					 Writer.WriteLine($"{enemy.Name} regained {Heal(enemy, .17f)} HP.");
				else Writer.WriteLine($"{enemy.Name} is already quite healthy and doesn't heal.");

				return;
			}
			default: {
				Writer.WriteLine("ERROR: Invalid enemyChoice inside block.");
				return;
			}
		}

	}

	internal static void Info() {
		Writer.Clear();
		Writer.CursorTop();
		player.Display();
		enemy.Display();
	}

	internal static bool Flee() {
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

			General.WaitForInput();
			Menu.Info();

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

}
