using System;
using System.Reflection.Metadata;
using DungeonLibrary;

namespace Dungeon;

class Program {

	public static Entity player = DefaultPlayer();

	static void Main(string[] args) {

		//=== TITLE/HEADER ===\\
		Signature.Header("THE DUNGEON OF NAVIA", "Your adventure awaits...");

		//=== VARIABLES/SETUP ===\\
		//player = CreateCharacter();
		WorldManager.CreateWorlds();
		MenuOptions menuChoice = MenuOptions.Move;

		//=== FIRST ROUND ===\\
		Console.Clear();
		StartDescription();

		//=== GAME LOOP ===\\
		while (true) {

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

			//choose action for next loop
			menuChoice = Menu.MenuSelect();
			if(menuChoice == MenuOptions.Quit) break;

			Console.Clear();
		}

		Signature.Footer();
		
	}

	private static Entity DefaultPlayer() {
		return new Entity("Name",
			new Race(RaceType.Human),
			new Armour(ArmourType.Medium),
			new Weapon(WeaponType.Sword));
	}

	private static Entity CreateCharacter() {
		Console.WriteLine("NAME YOUR CHARACTER");
		string nameChoice = Console.ReadLine();

		Array values = Enum.GetValues(typeof(RaceType));
		RaceType rType;

		int charConfirm = 2;
		do {
			Console.WriteLine("Choose your race:\n" +
			"(H) Human (default)\n" +
			"(E) Elf\n" +
			"(D) Dwarf\n" +
			"(G) Goblin\n" +
			"(O) Orc");
			int choice = Console.ReadLine().ToUpper().Trim() switch {
				"H" => 0,
				"E" => 1,
				"D" => 2,
				"G" => 3,
				"O" => 4,
				_ => 0
			};
			rType = (RaceType)values.GetValue(choice);

			Console.WriteLine($"You wish to be a {(RaceType)choice}? Y/N");
			do {
				charConfirm = Console.ReadLine().ToUpper().Trim() switch {
					"Y" => 0,
					"N" => 1,
					_	=> 2
				};
			} while(charConfirm == 2);

		} while(charConfirm != 0);

		return new Entity(nameChoice,
			new Race(rType),
			new Armour(ArmourType.Medium),
			new Weapon(WeaponType.Sword));
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
