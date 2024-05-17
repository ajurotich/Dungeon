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
}

public class Race {

	//=== FIELDS ===\\
	private RaceType _type;
	private float _maxHealth, _skill;

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
		}
	}

	//=== METHODS ===\\
	public static RaceType RandomType() {
		Random random = new Random();
		Array values = Enum.GetValues(typeof(RaceType));
		RaceType rType = (RaceType)values.GetValue(random.Next(values.Length));

		return rType;
	}

}
