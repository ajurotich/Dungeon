using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary;
using Common;

namespace Dungeon;

public class WorldManager {

	//=== WORLD DATA ===\\
	private static readonly string[] names = {
		"Frigid Passageway",
		"Overgrown Thicket",
		"Spectral Crypt",
		"Molten Corridor",
		"Damp Catacombs",
		"Deserted Tavern",
		"Forgotten Library",
		"Ritual Chambers",
		"Collapsed Mine Shaft",
		"Verdant Sanctuary",
	};
	private static readonly string[] descriptions = {
		"You find yourself in a narrow, icy corridor, the walls glistening with frost. The air is crisp, each breath forming a small cloud before dissipating. Jagged icicles hang precariously from the ceiling, occasionally dripping water onto the cold stone floor. The passage twists and turns ahead, promising further exploration into the frozen depths of the dungeon.",
		"Entering this chamber feels like stepping into a forgotten garden overrun by nature's embrace. Vines and creepers snake along the walls, their leaves brushing against your shoulders as you navigate the cramped space. Sunlight filters through cracks in the ceiling, casting dappled shadows on the lush foliage that carpets the floor. The air is thick with the scent of earth and vegetation, beckoning you to push deeper into the verdant labyrinth.",
		"The atmosphere in this dimly lit chamber is heavy with an aura of ancient mystique. Stone sarcophagi line the walls, their surfaces etched with faded runes and symbols of a forgotten language. Wisps of ethereal mist drift lazily through the air, coalescing into eerie shapes before dissipating into nothingness. Shadows dance along the uneven floor, hinting at hidden passages and secrets waiting to be unearthed.",
		"Your footsteps echo loudly in this narrow passage, the air shimmering with waves of heat rising from the molten rock that flows beneath the cracked stone floor. Glowing veins of lava trace intricate patterns along the walls, casting a fiery glow that bathes the corridor in a ruddy light. Wisps of smoke twist and curl around your ankles as you navigate the treacherous path, wary of any sudden eruptions or collapsing tunnels.",
		"Water drips steadily from the low ceiling of this claustrophobic chamber, pooling in stagnant puddles that dot the uneven stone floor. The air is thick with the musty scent of decay, the walls lined with ancient alcoves containing crumbling remains and forgotten relics. Faint echoes reverberate through the damp passageways, hinting at the presence of unseen chambers and hidden dangers lurking in the darkness.",
		"You step into the dimly lit interior of what was once a bustling tavern, now abandoned and left to decay. Dust motes dance in the slivers of sunlight filtering through boarded-up windows, casting long shadows across the worn wooden floor. Broken tables and chairs lie scattered haphazardly, remnants of past revelries now forgotten. The air is heavy with the musty scent of old ale and wood rot, a haunting reminder of the lively atmosphere that once filled this now desolate space.",
		"Rows of dusty bookshelves line the walls of this abandoned chamber, their contents obscured by the passage of time and neglect. Tattered scrolls and ancient tomes litter the floor, their pages yellowed with age and frayed at the edges. Cobwebs cling to the corners of the room, shrouding forgotten knowledge in layers of silk. A single shaft of sunlight pierces the gloom, illuminating a weathered desk covered in parchments and ink-stained quills, a silent sentinel amidst the ruins of forgotten lore.",
		"You find yourself in a chamber bathed in flickering candlelight, the air heavy with the scent of incense and arcane energies. Symbols and sigils adorn the walls, painted in vibrant hues of red and gold, their meanings shrouded in mystery and power. A stone altar stands at the center of the room, adorned with offerings of herbs, crystals, and other esoteric objects. Whispers of ancient rituals echo in the stillness, as if the very walls themselves hold the secrets of forgotten magics waiting to be unleashed.",
		"The air is thick with the smell of dust and earth as you navigate the treacherous remains of a collapsed mine shaft. Crumbling support beams and fallen debris litter the narrow passageways, creating a maze of precarious pathways and hidden dangers. Shafts of sunlight pierce through gaps in the rubble, casting shifting patterns of light and shadow on the rough-hewn walls. The distant rumble of shifting rock serves as a constant reminder of the unstable nature of your surroundings, urging you to tread carefully as you explore the forgotten depths.",
		"Nature has reclaimed this ancient sanctuary, its crumbling stone walls now obscured by a tangle of vibrant foliage and creeping vines. Shafts of sunlight filter through the dense canopy overhead, casting a warm glow on the moss-covered altar at the heart of the chamber. Fragments of weathered statues lie scattered amidst the undergrowth, their features worn away by centuries of wind and rain. The air is alive with the chirping of birds and the rustle of leaves, a tranquil oasis amidst the ruins of a forgotten civilization.",
	};
	private static World[] worldList = new World[names.Length];
	private static World currentWorld = new World(true);

	//=== FUNCTIONS ===\\
	public static void CreateWorlds() {
		for(int i = 0;i<names.Length && i<descriptions.Length;i++)
			worldList[i] = new World(names[i], descriptions[i]);
	}

	public static void StartDescription() {
		string[] descriptions = [
			"As you step into the room, you're enveloped by a swirling maelstrom of magic, a kaleidoscope of sights and sounds that transcends time and space. Ethereal wisps of mist dance through the air, carrying with them the fragrances of distant lands and forgotten realms. The walls shimmer and shift, morphing seamlessly from icy caverns to lush forests to crumbling ruins in the blink of an eye. Shafts of iridescent light pierce through the ever-changing scenery, casting prismatic reflections on the mirrored surface of a tranquil pool at the room's center. You feel the hum of ancient energies coursing through your veins, beckoning you to embark on a journey through realms both real and imagined. With a sense of wonder and anticipation, you take your first step into the boundless expanse of possibility that awaits beyond.",
			"Stepping into the chamber, you're surrounded by a symphony of magic, an ever-shifting tableau of wonder and mystery. Wisps of ethereal energy swirl around you, carrying whispers of distant lands and forgotten ages. The walls pulse with iridescent light, morphing from ancient catacombs to verdant meadows to celestial realms in an endless dance of creation. A shimmering portal hovers in the center of the room, its surface a gateway to infinite possibilities. With a sense of awe and anticipation, you take your first step into the boundless expanse of the unknown.",
			"Entering the room, you find yourself at the heart of a convergence of mystical energies, a swirling vortex of enchantment and wonder. The air crackles with arcane power, carrying echoes of distant realms and forgotten legends. The scenery shifts and changes before your eyes, blending icy mountains with sunlit valleys, ancient temples with enchanted forests. A shimmering gateway looms in the center of the chamber, a portal to realms beyond imagination. With a sense of excitement and curiosity, you take your first step into the boundless expanse of the multiverse.",
			"As you step into the chamber, you're greeted by an otherworldly scene, a crossroads of dreams and destinies. Wisps of shimmering mist drift through the air, carrying echoes of distant lands and forgotten memories. The walls shimmer with a soft glow, shifting from moonlit glades to starlit skies to shadowed alleys in an ever-changing panorama. A swirling vortex of light hovers in the center of the room, a gateway to realms beyond mortal comprehension. With a sense of wonder and anticipation, you take your first step into the boundless expanse of the unknown.",
			"Stepping into the room, you're surrounded by the boundless expanse of the cosmos, a tapestry of stars and secrets unfurled before you. Ethereal wisps of light dance through the air, carrying whispers of distant galaxies and forgotten constellations. The walls shimmer with celestial energies, shifting from cosmic voids to nebulous clouds to crystalline palaces in a mesmerizing display. A luminous gateway hangs in the center of the chamber, a portal to realms beyond the limits of mortal understanding. With a sense of awe and determination, you take your first step into the infinite expanse of the universe.",
		];

		Console.WriteLine(General.Wrap("Your quest begins at the foot of the Great Mountain of Navia, where a mystical door stands tall before you. You have trained hard for this. You take a deep breath and push past the entryway...\n"));
		Console.WriteLine(General.Wrap(descriptions[new Random().Next(descriptions.Length)]));

	}

	public static void ChooseWorlds() {

		int remainingWorlds = 0;
		foreach(World world in worldList) 
			if(!world.IsSearched) remainingWorlds++;

		General.Border();
		if(remainingWorlds !=0) {
			Console.WriteLine("Which realm would you like to travel to?\n");
			//Console.WriteLine("Default: stay in current world.\n");

			Random rand = new Random();
			World[] wOptions = new World[(remainingWorlds>=4) ? rand.Next(2,5):remainingWorlds];

			for(int i = 0; i<wOptions.Length && i<=remainingWorlds; i++) {
				int r = rand.Next(worldList.Length);

				if(!wOptions.Contains(worldList[r]) && !worldList[r].IsSearched)
					wOptions[i] = worldList[r];
				else {
					i--;
					continue;
				}

				Console.WriteLine($"{i+1}) {wOptions[i].Name}");
			}

			Console.WriteLine();
			while (true) {
				int selection;
				Console.Write(">> ");
				if (int.TryParse( Console.ReadLine().Trim(), out selection)) {
					if(0<selection && selection<=wOptions.Length)
						currentWorld = wOptions[--selection];
					break;
				}
			}

		}
		else {
			Console.WriteLine("All worlds explored.");
			//TODO maybe Boss Fight once all worlds are explored
		}

		if(currentWorld.Name == "" && currentWorld.Description == "") ChooseWorlds();

		Console.Clear();
		currentWorld.Display();

	}

	public static void SearchWorld() {
		General.Ellipsis("You search through the world and find");

		//currentWorld.DisplayAllObjects();
		Console.WriteLine();

		Object currentObject = currentWorld.Objects[currentWorld.SearchAmount-1];
		switch(currentObject) {
			case Armour a: {
				Armour pa = Program.player.Armour;

				Console.WriteLine($"Some {a} armour!\n");

				if(a.Name == pa.Name) {
					Console.WriteLine("It seems to be the same armour you're already wearing.\n");
					break;
				}

				Armour.CompareArmour(pa, a);

				int cursorHeight = Console.CursorTop;
				General.Border();
				Console.WriteLine("What would you like to do with it?\n");
				Console.WriteLine(
					"1) Equip found armour and discard current armour\n" +
					"2) Discard found armour and keep current armour\n");

				bool loop = true;
				while(loop) {
					Console.Write(">> ");

					switch(Console.ReadLine().Trim().ToUpper()) {
						case "1":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You discard the {pa} and equip the {a}.");
							Program.player.ChangeArmour(a);
							loop = false;
							break;

						case "2":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You decide to keep your current {pa} armour.");
							loop = false;
							break;

						default: break;
					}
				}

				break;
			}
			
			case Weapon w: {
				Weapon pw = Program.player.Weapon;

				Console.WriteLine($"A{((w.Mod == WeaponMod.Broken || w.Mod == WeaponMod.New) ? "" : "n")} {w}!");

				if(w.Name == pw.Name) {
					Console.WriteLine("It seems to be the same weapon you're already using.");
					break;
				}

				Weapon.CompareWeapon(pw, w);

				int cursorHeight = Console.CursorTop;
				General.Border();
				Console.WriteLine("Would you like to do with it?\n");
				Console.WriteLine(
					"1) Equip found weapon and discard current weapon\n" +
					"2) Discard found weapon and keep current weapon\n");

				bool loop = true;
				while(loop) {
					Console.Write(">> ");

					switch(Console.ReadLine().Trim().ToUpper()) {
						case "1":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You discard the {pw} and equip the {w}.");
							Program.player.ChangeWeapon(w);
							loop = false;
							break;

						case "2":
							Console.CursorTop = cursorHeight;
							Console.WriteLine($"You decide to keep your current {pw} weapon.");
							loop = false;
							break;

						default: break;
					}
				}

				break;
			}

			case Potion p:
				float healAmount = p.HealPercent * Program.player.Race.MaxHealth / 100;
					
				Console.WriteLine($"A {p.Descriptor} health potion!");

				if(Program.player.Health >= Program.player.Race.MaxHealth)
					Console.WriteLine($"It seems you are at full health already, but you {p.Verb} the whole thing anyway.");
				else 
					Console.WriteLine($"You {p.Verb} the entire potion and restore {(int)healAmount} HP!");

				Console.WriteLine($"\nYou are feeling {p.Descriptor}.\n");

				Program.player.Heal(healAmount);
				break;

			case Entity e:
				Console.WriteLine($"Nothing... but it seems a {Enum.GetName(e.Race.Type)} found you!\n");
				General.WaitForInput();

				Combat.Fight(e);

				break;

			default:
				Console.WriteLine("Unknown object found... what is this thing?");
				break;
		}

		if(Program.player.IsAlive) {
			Console.CursorTop = 28;
			if(currentWorld.IncrementSearch())
				Console.WriteLine("It seems you've found everything of value in this world...");
			else 
				Console.WriteLine("It seems there may be more to discover here...");
		}

	}

	public static World CurrentWorld => currentWorld;

}
