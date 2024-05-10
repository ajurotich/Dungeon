using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary;

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
		for(int i = 0; i<names.Length && i<descriptions.Length; i++)
			worldList[i] = new World(names[i], descriptions[i]);
	}

	public static void ChooseWorlds() {

		int remainingWorlds = 0;
		foreach(World world in worldList) 
			if(!world.IsSearched) remainingWorlds++;

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

			while(true) {
				int selection;
				if(int.TryParse( Console.ReadLine().Trim(), out selection)) {
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

		Console.WriteLine();
		currentWorld.Display();

	}

	public static void SearchWorld() {
		Console.Write("You search through the world and find");
		for(int i = 0; i < 3; i++) {
			System.Threading.Thread.Sleep(500);
			Console.Write(".");
		}

		//currentWorld.DisplayAllObjects();

		Object currentObject = currentWorld.Objects[currentWorld.SearchAmount-1];
		switch(currentObject) {
			case Armour a: {
				Console.WriteLine($"Some {Enum.GetName(a.Mod)} {Enum.GetName(a.Type)} Armour!\n");

				//todo display new and old armour stats before asking to swap

				Console.WriteLine("Would you like to do with it?");
				Console.WriteLine(
					"1) Equip found armour and discard current armour\n" +
					"2) Discard found armour and keep current armour\n");

				bool loop = true;
				while(loop) {
					switch(Console.ReadLine().Trim().ToUpper()) {
						case "1":
							Console.WriteLine($"You discard the {Enum.GetName(Program.player.Armour.Type)}" +
								$" and equip the {Enum.GetName(a.Type)}.");
							Program.player.ChangeArmor(a);
							loop = false;
							break;

						case "2":
							Console.WriteLine("You decide to keep your current armour.");
							loop = false;
							break;

						default: break;
					}
				}

				break;
			}

			case Weapon w: {
				Console.WriteLine($"A {Enum.GetName(w.WeaponMod)} {Enum.GetName(w.WeaponType)}!");

				//todo display new and old weapon stats before asking to swap

				Console.WriteLine("Would you like to do with it?");
				Console.WriteLine(
					"1) Equip found weapon and discard current weapon\n" +
					"2) Discard found weapon and keep current weapon\n");

				bool loop = true;
				while(loop) {
					switch(Console.ReadLine().Trim().ToUpper()) {
						case "1":
							Console.WriteLine($"You discard the {Enum.GetName(Program.player.Weapon.WeaponType)}" +
								$" and equip the {Enum.GetName(w.WeaponType)}.");
							Program.player.ChangeWeapon(w);
							loop = false;
							break;

						case "2":
							Console.WriteLine("You decide to keep your current weapon.");
							loop = false;
							break;

						default: break;
					}
				}

				break;
			}

			//TODO POTION OBJECT
			/*
			//case Potion p:
			//	Console.WriteLine($"A delicious health potion! You restore {p.HealAmount}!");
			//	Player.Heal(p.HealAmount);
			//	break;
			*/

			case Entity e:
				Console.WriteLine($"Nothing... but it seems a {Enum.GetName(e.Race.Type)} found you!");
				Console.WriteLine("--BATTLE--");
				//TODO FIGHT
				break;

			default:
				Console.WriteLine("Unknown Object Found... What is this thing?");
				break;
		}


		if(currentWorld.IncrementSearch())
			Console.WriteLine("\nIt seems you've found everything of value...");
		else 
			Console.WriteLine("\nIt seems there may be more to discover here...");

	}

	public static World CurrentWorld => currentWorld;
}