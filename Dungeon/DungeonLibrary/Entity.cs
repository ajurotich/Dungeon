using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary {

	public class Entity {

		//=== FIELDS ===\\
		private string _name;
		private float _health, _skill;
		private bool _isAlive;
		private Race _race;
		private Armour _armour;
		private Weapon _weapon;

		//=== PROPERTIES ===\\
		public string Name	 => _name;
		public float Health	 => _health;
		public float Skill	 => _skill;
		public bool IsAlive	 => _isAlive;
		public Race Race	 => _race;
		public Armour Armour => _armour;
		public Weapon Weapon => _weapon;

		//=== CTOR ===\\
		public Entity(string name, Race race, Armour armour, Weapon weapon) {
			_name = name;

			_race = race;
			_health = race.MaxHealth;
			_skill = race.MaxSkill;
			_isAlive = true;

			_armour = armour;
			_weapon = weapon;
		}

		//=== METHODS ===\\
		public float Damage(float damageAmount) {
			_health -= damageAmount;

			if(Health < 0) _isAlive = false;

			return Health;
		}
		public float Heal(float healAmount) {
			_health += healAmount;

			if(_health < Race.MaxHealth) _health = Race.MaxHealth;

			return Health;
		}
		//todo attack
		//todo block
		public void ChangeArmor(Armour armor) {
			_armour = armor;
		}
		public void ChangeWeapon(Weapon weapon) {
			_weapon = weapon; 
		}
	}
}
