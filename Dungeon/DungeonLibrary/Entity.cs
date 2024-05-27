using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
	public string Name	 => _name.ToUpper();
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
		_health = Math.Clamp((_health += MathF.Round(healAmount, 1)), 0, Race.MaxHealth);
		return Health;
	}

	public void Display() {
		for(int i= 0; i < 73; i++) Writer.Write("=");

		Writer.WriteLine($"\nDisplaying \'{Name}\' the {Enum.GetName(Race.Type)}:\n");

		int p = 16;

		Writer.WriteLine($"\t{("Health:").PadRight(p)}{Health}/{Race.MaxHealth}");
		Writer.WriteLine($"\t{("Armour:").PadRight(p)}{Armour.Name}\n" +
						 $"\t {("-Defense:").PadRight(p)}{Armour.Defense}\n" +
						 $"\t {("-Dodge:").PadRight(p)}{Armour.Dodge}");
		Writer.WriteLine($"\t{("Weapon:").PadRight(p)}{Weapon.Name}\n" +
						 $"\t {("-Damage:").PadRight(p)}{Weapon.Damage}\n" +
						 $"\t {("-Difficulty:").PadRight(p)}{Weapon.Difficulty}");

		for(int i= 0; i < 72; i++) Writer.Write("=");
		Writer.WriteLine("\n");
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

		Console.Title = "CHARACTER CREATOR";

		//=== VARIABLES ===\\
		string? nameChoice;
		RaceType rType;
		int charConfirm;

		do {
			Writer.Clear();
			Writer.WriteLine("YOUR CHARACTER");
			//Writer.WriteLine();

			//=== NAME ===\\
			Writer.Write("\nName: ");
			int[] cursorPos = [Console.CursorLeft, Console.CursorTop];

			while(true) { 
				Writer.CursorBottom();
				Writer.WriteLine("What would you like to name your character?\n");
				Writer.Write(">> ");
				nameChoice = Console.ReadLine();

				if(!string.IsNullOrEmpty(nameChoice.Trim())) break;
			}

			Console.SetCursorPosition(cursorPos[0], cursorPos[1]);
			Writer.Write(nameChoice.ToUpper());
			Writer.WriteLine();

			//=== RACE ===\\
			Writer.Write("Race: ");
			cursorPos = [Console.CursorLeft, Console.CursorTop];
			string input;

			while(true) {
				Writer.CursorBottom();
				Writer.WriteLine("Choose your race:\n");

				for(int i = 0; i <= (int)RaceType.Orc; i++)
					Writer.WriteLine($"{i+1}) {(RaceType)i}");

				Writer.WriteLine();
				Writer.Write(">> ");

				input = Console.ReadLine().Trim().ToUpper();
				if(Regex.IsMatch(input, "^[1-5]$")) break;
			}

			rType = input switch {
				"1" => RaceType.Human,
				"2" => RaceType.Elf,
				"3" => RaceType.Dwarf,
				"4" => RaceType.Goblin,
				"5" => RaceType.Orc,
				 _  => Race.RandomType()
			};			

			Console.SetCursorPosition(cursorPos[0], cursorPos[1]);
			Writer.Write(Enum.GetName(rType).ToUpper());


			//=== CONFIRMATION===\\
			do {
				Writer.CursorBottom();
				Writer.WriteLine($"Does this look correct?\n");
				Writer.WriteLine($"1) YES\n2) NO");

				Writer.Write("\n>> ");
				charConfirm = Console.ReadLine().Trim().ToUpper() switch {
					"1" => 0,
					"2" => 1,
					_ => 2
				};
			} while(charConfirm == 2);

		} while(charConfirm != 0);

		return new Player(nameChoice, new Race(rType));
	}

	public void IncrementKillCount() {
		_killCount++;
	}

	public void ChangeArmour(Armour armour) {
		_armour = armour;
	}

	public void ChangeWeapon(Weapon weapon) {
		_weapon = weapon;
	}

}
