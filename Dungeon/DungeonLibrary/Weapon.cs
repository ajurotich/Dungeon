using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary {

	public enum WeaponType {
		Daggers, //low damage, easy
		Sword, //midlow damage, pretty easy
		Axe, // midhigh damage
		Hammer // hard
	}
	public enum WeaponMod {
		Epic,
		New,
		Used,
		Old,
		Broken
	}

	public class Weapon {

		//=== FIELDS ===\\
		private WeaponType _type;
		private WeaponMod _mod;
		private float _damage, _difficulty;

		//=== PROPS ===\\
		public WeaponMod WeaponMod		=> _mod;
		public WeaponType WeaponType	=> _type;
		public float Damage				=> _damage;
		public float Difficulty			=> _difficulty;

		//=== CTOR ===\\
		public Weapon(WeaponType type) {
			_type = type;
			_mod = RandomMod();

			switch(_type) {
				case WeaponType.Daggers:
					_damage = 10;
					_difficulty = 5;
					break;
				case WeaponType.Sword:
					_damage = 15;
					_difficulty = 10;
					break;
				case WeaponType.Axe:
					_damage = 20;
					_difficulty = 15;
					break;
				case WeaponType.Hammer:
					_damage = 25;
					_difficulty = 20;
					break;
			}
			switch(_mod) {
				case WeaponMod.Epic:
					_damage *= 1.15f;
					break;
				case WeaponMod.New:
					_damage *= 1.05f;
					break;
				case WeaponMod.Used:
					break;
				case WeaponMod.Old:
					_damage *= .95f;
					break;
				case WeaponMod.Broken:
					_damage *= .85f;
					break;
			}
		}

		//=== METHODS ===\\
		public static WeaponType RandomType() {
			Random random = new Random();
			Array values = Enum.GetValues(typeof(WeaponType));
			WeaponType rType = (WeaponType)values.GetValue(random.Next(values.Length));

			return rType;
		}
		public WeaponMod RandomMod() {
			Random random = new Random();
			Array values = Enum.GetValues(typeof(WeaponMod));
			WeaponMod rType = (WeaponMod)values.GetValue(random.Next(values.Length));

			return rType;
		}
		public static void test() {

		}
	}
}
