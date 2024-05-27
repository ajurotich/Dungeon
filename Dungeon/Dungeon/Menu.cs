using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using DungeonLibrary;

namespace Dungeon;

public enum MenuOptions {
	Search,
	Move,
	Self,
	Info,
	Quit,
}

public class Menu {

	public static MenuOptions MenuSelect() {
		Writer.CursorBottom();

		Writer.WriteLine("What would you like to do?\n");
		for(int i = 0; i <= (int)MenuOptions.Quit; i++)
			Writer.WriteLine($"{i+1}) {(MenuOptions)i}");

		Writer.Write("\n>> ");
		return (MenuOptions)((int.TryParse(Console.ReadLine().Trim(), out int num) && --num>=0 && num<=(int)MenuOptions.Quit) ? num : (int)MenuOptions.Info);

	}

	public static void Move() {
		Writer.Title = TitleOptions.MOVE;
		Writer.Clear();
		Writer.CursorTop();

		if(!WorldManager.CurrentWorld.IsSearched) {
			Writer.WriteLine("There's more to discover here, but you may return later.\n");

			while(true) {
				Writer.CursorBottom();
				Writer.WriteLine($"Are you sure you want to leave?\n");
				Writer.WriteLine($"1) YES\n2) NO");

				Writer.Write("\n>> ");
				if (int.TryParse (Console.ReadLine().Trim().ToUpper(), out int input))
					if(input == 1) break;
					else return;
			}
		}

		WorldManager.ChooseWorlds();
	}

	public static void Search() {
		Writer.Title = TitleOptions.SEARCH;
		Writer.Clear();
		Writer.CursorTop();

		if(!WorldManager.CurrentWorld.IsSearched) WorldManager.SearchWorld();
		else {
			Writer.Ellipsis("You search through the world and find");

			Writer.WriteLine("You try to search, but it seems there's nothing else of value here.\n" +
				"World complete!");
			WorldManager.remainingWorlds--;
		}

	}

	public static void Self() {
		Writer.Title = TitleOptions.SELF;
		Writer.Clear();
		Writer.CursorTop();

		Program.player.Display();
	}

	public static void Info() {
		Writer.Title = TitleOptions.INFO;
		Writer.Clear();
		Writer.CursorTop();

		WorldManager.CurrentWorld.Display();
	}
}
