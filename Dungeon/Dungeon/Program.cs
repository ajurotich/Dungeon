using System;
using System.Reflection.Metadata;
using DungeonLibrary;

namespace Dungeon;

class Program {

	public static Player player = new Player();

	static void Main(string[] args) {

		//=== TITLE/HEADER ===\\
		Signature.Header("THE DUNGEON OF NAVIA", "Your adventure awaits...");

		//=== VARIABLES/SETUP ===\\
		//player = Player.CreateCharacter();
		WorldManager.CreateWorlds();

		//=== FIRST ROUND ===\\
		Console.Clear();
		StartDescription();
		MenuOptions menuChoice = MenuOptions.Move;

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

			//player.Display();

			//choose action for next loop
			menuChoice = Menu.MenuSelect();
			if(menuChoice == MenuOptions.Quit) break;

			Console.Clear();
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

		Console.WriteLine("Welcome to the realm of Navia.\n");
		Console.WriteLine(Signature.Wrap("Your quest begins at the foot of the Great Mountain of Navia, where a mystical door stands tall before you. You have trained hard for this. You take a deep breath and push past the entryway...\n", 80));
		Console.WriteLine(Signature.Wrap(descriptions[new Random().Next(descriptions.Length)], 80));

		Console.WriteLine("\n\nPress any key to continue...");
		Console.ReadKey(false);
	}

}
