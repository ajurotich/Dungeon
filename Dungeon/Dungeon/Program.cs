using System;
using System.Reflection.Metadata;
using DungeonLibrary;
using Common;

namespace Dungeon;

class Program {

	public static Player player = new Player();

	static void Main(string[] args) {

		//=== WINDOW SET UP ===\\
		Writer.WindowSetup();

		//=== VARIABLES/SETUP ===\\
		player = Player.CreateCharacter();
		Writer.Clear();
		Writer.Ellipsis("Your adventure awaits");
		WorldManager.CreateWorlds();

		//=== FIRST ROUND ===\\
		WorldManager.StartDescription();
		General.WaitForInput();
		Menu.Move();
		MenuOptions menuChoice = MenuOptions.Info;

		//=== GAME LOOP ===\\
		while(true) {

			//does action
			switch (menuChoice) {
				case MenuOptions.Info:
					Menu.Info();
					break;
				case MenuOptions.Move:
					Menu.Move();
					break;
				case MenuOptions.Search:
					Menu.Search();
					break;
				default:
					Writer.Title = TitleOptions.NAVIA;
					Console.WriteLine("Unknown command.");
					break;
			}

			//check if player is dead
			if(!player.IsAlive)	break;

			//choose action for next loop
			menuChoice = Menu.MenuSelect();
			if(menuChoice == MenuOptions.Quit) break;

			Writer.Clear();
		}

		Writer.Title = TitleOptions.QUIT;

		//=== GAME RESULTS ===\\
		if(player.IsAlive) {
			Writer.Clear();
			Writer.WriteLine("\nYou completed your quest! Congratulations!");
			Writer.WriteLine($"\nYour final kill count: {player.KillCount}");
        }
		else {
			Writer.Clear();
			Writer.WriteLine("\nUnfortunately, your quest fell short. You were slain in battle.");
			Writer.WriteLine($"\nYour final kill count: {player.KillCount}");
        }

		//=== CREDITS ===\\
		General.WaitForInput();
		General.Footer();

	}

}
