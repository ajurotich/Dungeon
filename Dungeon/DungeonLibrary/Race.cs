using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary {

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
		private byte _maxHealth, _maxSkill;

		//=== PROPS ===\\
		public RaceType Type	=> _type;
		public byte MaxHealth	=> _maxHealth;
		public byte MaxSkill	=> _maxSkill;

		//=== CTOR ===\\
		public Race(RaceType type){
			_type = type;

			switch(_type) {
				case RaceType.Human:
					_maxHealth = 50;
					_maxSkill  = 50;
					break;
				case RaceType.Elf:
					_maxHealth = 30;
					_maxSkill  = 70;
					break;
				case RaceType.Dwarf:
					_maxHealth = 70;
					_maxSkill  = 30;
					break;
				case RaceType.Goblin:
					_maxHealth = 20;
					_maxSkill  = 80;
					break;
				case RaceType.Orc:
					_maxHealth = 80;
					_maxSkill  = 20;
					break;
			}
		}

		//=== METHODS ===\\
		public RaceType RandomType() {
			Random random = new Random();
			Array values = Enum.GetValues(typeof(RaceType));
			RaceType rType = (RaceType)values.GetValue(random.Next(values.Length));

			return rType;
		}

	}
}
