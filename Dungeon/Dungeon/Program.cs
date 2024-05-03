using System;
using DungeonLibrary;

namespace Dungeon {
	class Program {
		static void Main(string[] args) {

			//=== TITLE/HEADER ===\\
			Signature.Header("THE DUNGEON OF NAVIA", "Your adventure awaits...");

            //=== VARIABLES ===\\
			int score = 0;
			Entity player = CreateCharacter();

			

			//=== GAME LOOP ===\\
			Console.ReadLine();
#if false
			//TODO game loop
			// - exit condition
			do {
				//TODO generate random room

				//TODO select monster

				do {
					//TODO gameplay menu options
					switch("") {
						case "combat": {
							//TODO combat
							break;
						}

						case "runaway": {
							//TODO run away
							break;
						}

						case "playerinfo": {
							//TODO player info
							break;
						}

						case "monsterinfo": {
							//TODO monsterinfo
							break;
						}

						case "checklife": {
							//TODO check life
							break;
						}

						case "exit": {
							//TODO exit
							break;
						}

						default: {
							//TODO default/invalid
							break;
						}


					}

					// exit condition

				} while(false);

				//TODO output final score

			} while(false);
#endif

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

				Console.WriteLine($"You wish to be a {Enum.GetName(typeof(RaceType), choice)}? Y/N");
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
}
