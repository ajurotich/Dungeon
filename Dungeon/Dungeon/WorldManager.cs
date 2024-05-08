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
		Random rand = new Random();

		for(int i = 0; i<names.Length && i<descriptions.Length; i++)
			worldList[i] = new World(names[i], descriptions[i], rand.Next(2, 5));

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

	public static World CurrentWorld => currentWorld;
}