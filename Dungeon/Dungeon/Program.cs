using System;
using DungeonLibrary;

namespace Dungeon {
	class Program {
		static void Main(string[] args) {

			//=== TITLE/HEADER ===\\
			Signature.Header("DUNGEON", "Your adventure awaits...");

			//=== VARIABLES ===\\
			int score = 0;

			//TODO player


			//TODO weapons


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

			Signature.Footer();

		}
	}
}
