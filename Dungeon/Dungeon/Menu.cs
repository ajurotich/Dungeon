using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon;

public enum MenuOptions {
	Info,
	Search,
	Move,
	Quit,
}

public class Menu {

	public static MenuOptions MenuSelect() {
		Common.General.Border();

		Console.WriteLine("What would you like to do?\n");
		for(int i = 0; i <= (int)MenuOptions.Quit; i++) 
			Console.WriteLine($"{i+1}) {(MenuOptions)i}");

        Console.Write("\n>> ");
        return (MenuOptions)((int.TryParse(Console.ReadLine().Trim(), out int num) && --num>0 && num<=(int)MenuOptions.Quit) ? num : 0);

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
