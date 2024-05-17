using System;
using System.Reflection.Metadata;
using DungeonLibrary;

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
		Signature.Header("THE DUNGEON OF NAVIA", "Your adventure awaits...");

		//=== VARIABLES/SETUP ===\\
		player = Player.CreateCharacter();
		//Combat.Fight(new Entity("enemy", new Race(Race.RandomType()), new Armour(Armour.RandomType()), new Weapon(Weapon.RandomType())));
		WorldManager.CreateWorlds();

		//=== FIRST ROUND ===\\
		StartDescription();
		Console.ReadLine();
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

		if(player.IsAlive) {
            Console.WriteLine("You completed your quest! Congratulations!");
            Console.WriteLine($"Your final kill count: {player.KillCount}");
        }
		else {
            Console.WriteLine("Unfortunately, your quest fell short. You were slain in battle.");
            Console.WriteLine($"Your final kill count: {player.KillCount}");
        }


		Signature.Footer();
		
	}

	private static void StartDescription() {
		string[] descriptions = new string[] {
			"entryroom1",
			"entryroom2",
			"entryroom3",
			"entryroom4",
			"entryroom5",
		};

		Console.WriteLine(Signature.Wrap("Your quest begins at the foot of the Great Mountain of Navia, where a mystical door stands tall before you. You have trained hard for this. You take a deep breath and push past the entryway...\n", 80));
		Console.WriteLine(Signature.Wrap(descriptions[new Random().Next(descriptions.Length)], 80));

		Console.Write("\n\nPress any key to continue...");
		
	}

}
