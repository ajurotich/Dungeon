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
		//TODO menuoption : info
		Console.WriteLine("===INFO===");
    }

	public static void Move() {
		//TODO menuoption : move
		Console.WriteLine("===MOVE===");
	}

	public static void Search() {
		//TODO menuoption : search
		Console.WriteLine("===SEARCH===");
	}

}
