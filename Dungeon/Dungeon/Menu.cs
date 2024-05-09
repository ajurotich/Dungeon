using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon;

public enum MenuOptions {
	Info,
	Move,
	Search,
	Quit,
}

public class Menu {

	public static MenuOptions MenuSelect() {
		Console.WriteLine("\nWhat would you like to do?");
		for(int i = 0; i < 4; i++) 
			Console.WriteLine($"{i+1}) {(MenuOptions)i}");

		switch(Console.ReadLine().ToUpper().Trim() ) {
			case "1":
			case "I":
			case "INFO":
				return MenuOptions.Info;

			case "2":
			case "M":
			case "MOVE":
				return MenuOptions.Move;

			case "3":
			case "S":
			case "SEARCH":
				return MenuOptions.Search;

			case "4":
			case "Q":
			case "QUIT":
				return MenuOptions.Quit;

			default:
				break;
		}

		return MenuOptions.Info;
	}

	public static void Info() {
		Console.Clear();
		Console.WriteLine("\n===INFO===\n");
		WorldManager.CurrentWorld.Display();
    }

	public static void Move() {
		Console.Clear();
		Console.WriteLine("\n===MOVE===\n");

		if(!WorldManager.CurrentWorld.IsSearched) {
            Console.WriteLine("There's more to discover here, but you may return later.\n" +
				"Are you sure you want to leave? Y/N");

			bool loop = true;
            while (loop) 
				switch(Console.ReadLine().ToUpper().Trim()) {
					case "Y":
					case "YES":
						loop = false;
						break;
					case "NO":
					case "N":
						return;
					default: break;
				}
        }

		WorldManager.ChooseWorlds();
	}

	public static void Search() {
		Console.Clear();
		Console.WriteLine("\n===SEARCH===\n");

		if(!WorldManager.CurrentWorld.IsSearched) WorldManager.SearchWorld();
		else Console.WriteLine("You try to search, but it seems there's nothing else of value here.\n" +
				"World complete!");

        }

}
