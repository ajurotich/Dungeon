using System;
using System.Reflection.Metadata;
using DungeonLibrary;
using Common;

namespace Dungeon;

class Program {

	public static Player player = new Player();

	static void Main(string[] args) {

		//=== WINDOW SET UP ===\\
		Console.SetWindowSize(80, 40);
#pragma warning disable CA1416 // Validate platform compatibility
		Console.BufferWidth = 80;
		Console.BufferHeight = 40;
#pragma warning restore CA1416 // Validate platform compatibility

		//=== VARIABLES/SETUP ===\\
		player = Player.CreateCharacter();
		Console.Clear();
		General.Header("THE DUNGEON OF NAVIA", "Your adventure awaits...");
		//Combat.Fight(new Entity("enemy", new Race(Race.RandomType()), new Armour(Armour.RandomType()), new Weapon(Weapon.RandomType())));
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
					Console.WriteLine("Unknown command.");
					break;
			}

			//check if player is dead
			if(!player.IsAlive)	break;

			//player.Display();

			//choose action for next loop
			menuChoice = Menu.MenuSelect();
			if(menuChoice == MenuOptions.Quit) break;

			Console.Clear();
		}

		//=== GAME RESULTS ===\\
		if(player.IsAlive) {
			Console.Clear();
            Console.WriteLine("\nYou completed your quest! Congratulations!");
            Console.WriteLine($"Your final kill count: {player.KillCount}");
        }
		else {
			Console.Clear();
            Console.WriteLine("\nUnfortunately, your quest fell short. You were slain in battle.");
            Console.WriteLine($"Your final kill count: {player.KillCount}");
        }

		//=== CREDITS ===\\
		General.WaitForInput();
		General.Footer();

	}

}
