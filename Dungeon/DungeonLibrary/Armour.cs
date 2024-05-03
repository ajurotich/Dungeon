using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary {

	public enum ArmourType {
		Heavy,
		Medium,
		Light
	}
	public enum ArmourMod {
		Epic,
		New,
		Used,
		Old,
		Broken
	}

	public class Armour {

		//=== FIELDS ===\\
		private ArmourType _type;
		private ArmourMod _mod;
		private float _dodgeMod, _defenseMod;

		//=== PROPS ===\\
		public ArmourType Type	=> _type;
		public ArmourMod Mod	=> _mod;
		public float DodgeMod	=> _dodgeMod;
		public float DefenseMod	=> _defenseMod;

		//=== CTOR ===\\
		public Armour(ArmourType type) {
			_type = type;
			_mod = RandomMod();

			switch (_type) {
				case ArmourType.Light:
					_dodgeMod = .3f;
					_defenseMod = .2f;
					break;
				case ArmourType.Medium:
					_dodgeMod = .2f;
					_defenseMod = .4f;
					break;
				case ArmourType.Heavy:
					_dodgeMod = .1f;
					_defenseMod = .6f;
					break;
			}
			switch (_mod) {
			case ArmourMod.Epic:
				_defenseMod *= 1.15f;
				break;
			case ArmourMod.New:
				_defenseMod *= 1.05f;
				break;
			case ArmourMod.Used:
				break;
			case ArmourMod.Old:
				_defenseMod *= .95f;
				break;
			case ArmourMod.Broken:
				_defenseMod *= .85f;
				break;
			}
		}

		//=== METHODS ===\\
		public ArmourType RandomType() {
			Random random = new Random();
			Array values = Enum.GetValues(typeof(ArmourType));
			ArmourType rType = (ArmourType)values.GetValue(random.Next(values.Length));

			return rType;
		}
		public ArmourMod RandomMod() {
			Random random = new Random();
			Array values = Enum.GetValues(typeof(ArmourMod));
			ArmourMod rType = (ArmourMod)values.GetValue(random.Next(values.Length));

			return rType;
		}

	}
}
