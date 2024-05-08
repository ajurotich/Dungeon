using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public class World {
	
	//=== FIELDS ===\\
	private string[] _title = new string[]{
		"Frigid Passageway",
		"Overgrown Thicket",
		"Spectral Crypt",
		"Molten Corridor",
		"Damp Catacombs",
		"Deserted Tavern",
		"Forgotten Library",
		"Ritual Chambers",
		"Collapsed Mine Shaf",
		"Overgrown Sanctuary",
	};
	private string[] _descriptions = new string[]{
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

	private byte searchAmount;
	private bool isSearched = false;

	//=== PROPS ===\\


	//=== CTOR ===\\

	//=== METHODS ===\\

	public static string Wrapper(string v, int size) {
		v = v.TrimStart();
		if(v.Length <= size)
			return v;
		var nextspace = v.LastIndexOf(' ', size);
		if(-1 == nextspace)
			nextspace = Math.Min(v.Length, size);
		return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
		"" : "\n" + Wrapper(v.Substring(nextspace), size));
	}
}
