using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public enum RaceType {
	Human,
	Elf,
	Dwarf,
	Goblin,
	Orc,
	Dragon
}

public class Race {

	//=== FIELDS ===\\
	private RaceType _type;
	protected float _maxHealth, _skill;

	//=== PROPS ===\\
	public RaceType Type	=> _type;
	public float MaxHealth	=> _maxHealth;
	public float Skill	=> _skill;

	//=== CTOR ===\\
	public Race(RaceType type){
		_type = type;

		switch(_type) {
			case RaceType.Goblin:
				_maxHealth  = 40;
				_skill		= 18f;
				break;
			case RaceType.Elf:
				_maxHealth  = 50;
				_skill		= 16f;
				break;
			case RaceType.Human:
				_maxHealth  = 60;
				_skill		= 14;
				break;
			case RaceType.Dwarf:
				_maxHealth  = 70;
				_skill		= 12f;
				break;
			case RaceType.Orc:
				_maxHealth  = 80;
				_skill		= 10f;
				break;
			case RaceType.Dragon:
				_maxHealth  = 100;
				_skill		= 20f;
				break;
		}
	}

	//=== METHODS ===\\
	public static RaceType RandomType() {
		Random random = new Random();
		Array values = Enum.GetValues(typeof(RaceType));
		RaceType rType = (RaceType)values.GetValue(random.Next(values.Length-1));

		return rType;
	}

}


public class RaceNames {

	private static readonly string[] _humanNames = {
		"human1",
		"human2",
		"human3",
	};
	private static readonly string[] _elfNames = {
		"elf1",
		"elf2",
		"elf3",
		"elf4",
		"elf5",
		"elf6",
		"elf7",
		"elf8",
	};
	private static readonly string[] _dwarfNames = {
		"dwarf1",
		"dwarf2",
		"dwarf3",
		"dwarf4",
	};
	private static readonly string[] _goblinNames = {
		"goblin1",
		"goblin2",
		"goblin3",
		"goblin4",
		"goblin5",
	};
	private static readonly string[] _orcNames = {
		"orc1",
		"orc2",
		"orc3",
		"orc4",
		"orc5",
		"orc6",
		"orc7",
	};

	public static string GetRandomName(RaceType rt) {
		Random ran = new Random();

		return rt switch {
			RaceType.Human	=> (_humanNames [ran.Next(0, _humanNames .Length-1)]).ToUpper(),
			RaceType.Elf	=> (_elfNames   [ran.Next(0, _elfNames   .Length-1)]).ToUpper(),
			RaceType.Dwarf	=> (_dwarfNames [ran.Next(0, _dwarfNames .Length-1)]).ToUpper(),
			RaceType.Goblin => (_goblinNames[ran.Next(0, _goblinNames.Length-1)]).ToUpper(),
			RaceType.Orc	=> (_orcNames   [ran.Next(0, _orcNames   .Length-1)]).ToUpper(),
			_ => "NAME"
		};

	}

}