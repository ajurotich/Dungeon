using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public class Entity {

	//=== FIELDS ===\\
	private string _name;
	protected float _health;
	private bool _isAlive;
	private Race _race;
	protected Armour _armour;
	protected Weapon _weapon;

	//=== PROPERTIES ===\\
	public string Name	 => _name;
	public float Health	 => MathF.Round(_health, 1);
	public bool IsAlive	 => _isAlive;
	public Race Race	 => _race;
	public Armour Armour => _armour;
	public Weapon Weapon => _weapon;

	//=== CTOR ===\\
	public Entity(string name, Race race, Armour armour, Weapon weapon) {
		_name = name;

		_race = race;
		_health = race.MaxHealth;
		_isAlive = true;

		_armour = armour;
		_weapon = weapon;
	}

	//=== METHODS ===\\
	public float Damage(float damageAmount) {
		if(damageAmount <= 0) return Health;

		_health -= MathF.Round(damageAmount, 1);

		if(Health <= 0) _isAlive = false;

		return Health;
	}

	public float Heal(float healAmount) {
		Math.Clamp((_health += MathF.Round(healAmount, 1)), 0, Race.MaxHealth);
		return Health;
	}

	public void Display() {
		for(int i= 0; i < 80; i++) Console.Write("=");

        Console.WriteLine($"\nDisplaying \'{Name}\' the {Enum.GetName(Race.Type)}:\n");

		int p = 16;

		Console.WriteLine($"{("Health:").PadRight(p)}{Health}/{Race.MaxHealth}");
		Console.WriteLine($"{("Skill:").PadRight(p)}{Race.Skill}");
		Console.WriteLine($"{("Armour:").PadRight(p)}{Armour.Name}\n" +
						$" {(">Defense:").PadRight(p)}{Armour.Defense}\n" +
						$" {(">Dodge:").PadRight(p)}{Armour.Dodge}");
		Console.WriteLine($"{("Weapon:").PadRight(p)}{Weapon.Name}\n" +
						$" {(">Damage:").PadRight(p)}{Weapon.Damage}\n" +
						$" {(">Difficulty:").PadRight(p)}{Weapon.Difficulty}");

		for(int i= 0; i < 80; i++) Console.Write("=");
		Console.WriteLine("\n");
    }
	
}

public class Player : Entity {

	//=== FIELDS ===\\
	private int _killCount = 0;

	//=== PROPERTIES ===\\
	public int KillCount => _killCount;

	//=== CTOR ===\\
	public Player():
		base("name",
		new Race(RaceType.Human),
		new Armour(ArmourType.Medium),
		new Weapon(WeaponType.Sword)) {
	}
	public Player(string name, Race race)
		: base(name, race, new Armour(ArmourType.Medium), new Weapon(WeaponType.Sword)) {}

	//=== METHODS ===\\
	public static Player CreateCharacter() {
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

			Console.WriteLine($"You wish to be a {(RaceType)choice}? Y/N");
			do {
				charConfirm = Console.ReadLine().ToUpper().Trim() switch {
					"Y" => 0,
					"N" => 1,
					_ => 2
				};
			} while(charConfirm == 2);

		} while(charConfirm != 0);

		return new Player(nameChoice, new Race(rType));
	}

	public void IncrementKillCount() {
		_killCount++;
	}


	public void ChangeArmor(Armour armor) {
		_armour = armor;
	}

	public void ChangeWeapon(Weapon weapon) {
		_weapon = weapon;
	}

}
