using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary;
using Common;

namespace Dungeon;

public class WorldManager {

	//=== WORLD DATA ===\\
	private static readonly string[] names = {
		"world name a",
		"world name b",
		"world name c",
		"world name d",
		"world name e",
		"world name f",
		"world name g",
		"world name h",
		"world name i",
		"world name j",
	};
	private static readonly string[] descriptions = {
		"world description 0",
		"world description 1",
		"world description 2",
		"world description 3",
		"world description 4",
		"world description 5",
		"world description 6",
		"world description 7",
		"world description 8",
		"world description 9",
	};
	private static World[] worldList = new World[names.Length];
	private static World currentWorld = new World(true);

	//=== FUNCTIONS ===\\
	public static void CreateWorlds() {
		for(int i = 0;i<names.Length && i<descriptions.Length;i++)
			worldList[i] = new World(names[i], descriptions[i]);
	}

	public static void StartDescription() {
		string[] descriptions = new string[] {
			"entryroom1",
			"entryroom2",
			"entryroom3",
			"entryroom4",
			"entryroom5",
		};

		Console.WriteLine(General.Wrap("Your quest begins at the foot of the Great Mountain of Navia, where a mystical door stands tall before you. You have trained hard for this. You take a deep breath and push past the entryway...\n"));
		Console.WriteLine(General.Wrap(descriptions[new Random().Next(descriptions.Length)]));

	}

	public static void ChooseWorlds() {

		int remainingWorlds = 0;
		foreach(World world in worldList) 
			if(!world.IsSearched) remainingWorlds++;

		General.Border();
		if(remainingWorlds !=0) {
			Console.WriteLine("Which realm would you like to travel to?\n");
			//Console.WriteLine("Default: stay in current world.\n");

			Random rand = new Random();
			World[] wOptions = new World[(remainingWorlds>=4) ? rand.Next(2,5):remainingWorlds];

			for(int i = 0; i<wOptions.Length && i<=remainingWorlds; i++) {
				int r = rand.Next(worldList.Length);

				if(!wOptions.Contains(worldList[r]) && !worldList[r].IsSearched)
					wOptions[i] = worldList[r];
				else {
					i--;
					continue;
				}

				Console.WriteLine($"{i+1}) {wOptions[i].Name}");
			}

            Console.WriteLine();
            while (true) {
				int selection;
                Console.Write(">> ");
                if (int.TryParse( Console.ReadLine().Trim(), out selection)) {
					if(0<selection && selection<=wOptions.Length)
						currentWorld = wOptions[--selection];
					break;
				}
			}

		}
		else {
			Console.WriteLine("All worlds explored.");
			//TODO maybe Boss Fight once all worlds are explored
		}

		if(currentWorld.Name == "" && currentWorld.Description == "") ChooseWorlds();

		Console.Clear();
		currentWorld.Display();

	}

	public static void SearchWorld() {
		General.Ellipsis("You search through the world and find");

        //currentWorld.DisplayAllObjects();
        Console.WriteLine();

        Object currentObject = currentWorld.Objects[currentWorld.SearchAmount-1];
		switch(currentObject) {
			case Armour a: {
				Armour pa = Program.player.Armour;

				Console.WriteLine($"Some {a} armour!\n");

				if(a.Name == pa.Name) {
					Console.WriteLine("It seems to be the same armour you're already wearing.\n");
					break;
				}

				Armour.CompareArmour(pa, a);

				int cursorHeight = Console.CursorTop;
				General.Border();
				Console.WriteLine("What would you like to do with it?\n");
				Console.WriteLine(
					"1) Equip found armour and discard current armour\n" +
					"2) Discard found armour and keep current armour\n");

				bool loop = true;
				while(loop) {
					Console.Write(">> ");

					switch(Console.ReadLine().Trim().ToUpper()) {
						case "1":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You discard the {pa} and equip the {a}.");
							Program.player.ChangeArmor(a);
							loop = false;
							break;

						case "2":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You decide to keep your current {pa} armour.");
							loop = false;
							break;

						default: break;
					}
				}

				break;
			}
			
			case Weapon w: {
				Weapon pw = Program.player.Weapon;

				Console.WriteLine($"A{((w.Mod == WeaponMod.Broken || w.Mod == WeaponMod.New) ? "" : "n")} {w}!");

				if(w.Name == pw.Name) {
					Console.WriteLine("It seems to be the same weapon you're already using.");
					break;
				}

				Weapon.CompareWeapon(pw, w);

				int cursorHeight = Console.CursorTop;
				General.Border();
				Console.WriteLine("Would you like to do with it?\n");
				Console.WriteLine(
					"1) Equip found weapon and discard current weapon\n" +
					"2) Discard found weapon and keep current weapon\n");

				bool loop = true;
				while(loop) {
					Console.Write(">> ");

					switch(Console.ReadLine().Trim().ToUpper()) {
						case "1":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You discard the {pw} and equip the {w}.");
							Program.player.ChangeWeapon(w);
							loop = false;
							break;

						case "2":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You decide to keep your current {pw} weapon.");
							loop = false;
							break;

						default: break;
					}
				}

				break;
			}

			case Potion p:
				float healAmount = p.HealPercent * Program.player.Race.MaxHealth / 100;
					
				Console.WriteLine($"A {p.Descriptor} health potion!");

				if(Program.player.Health >= Program.player.Race.MaxHealth)
					Console.WriteLine($"It seems you are at full health already, but you {p.Verb} the whole thing anyway.");
				else 
					Console.WriteLine($"You {p.Verb} the entire potion and restore {(int)healAmount} HP!");

				Console.WriteLine($"\nYou are feeling {p.Descriptor}.\n");

				Program.player.Heal(healAmount);
				break;

			case Entity e:
				Console.WriteLine($"Nothing... but it seems a {Enum.GetName(e.Race.Type)} found you!\n");
                General.WaitForInput();

				Combat.Fight(e);

				break;

			default:
				Console.WriteLine("Unknown object found... what is this thing?");
				break;
		}

		if(Program.player.IsAlive) {
			Console.CursorTop = 28;
			if(currentWorld.IncrementSearch())
				Console.WriteLine("It seems you've found everything of value in this world...");
			else 
				Console.WriteLine("It seems there may be more to discover here...");
		}

	}

	public static World CurrentWorld => currentWorld;

}
