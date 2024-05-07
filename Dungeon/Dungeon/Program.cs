using System;
using DungeonLibrary;

namespace Dungeon;

class Program {
	static void Main(string[] args) {

		//=== TITLE/HEADER ===\\
		Signature.Header("THE DUNGEON OF NAVIA", "Your adventure awaits...");

        //=== VARIABLES ===\\
		int score = 0;
		Entity player = CreateCharacter();
		MenuOptions menuChoice = MenuOptions.Info;

		//=== FIRST ROUND ===\\
		Console.Clear();
		//TODO generate room
		Console.WriteLine("generate room");

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

			//choose action for next loop
			//done at end of 
			menuChoice = Menu.MenuSelect();
			if(menuChoice == MenuOptions.Quit) break;

			Console.Clear();
		}

		Signature.Footer();
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

		return new Entity(nameChoice, new Race(rType),
			new Armour(ArmourType.Medium),
			new Weapon(WeaponType.Sword));
	}

}
