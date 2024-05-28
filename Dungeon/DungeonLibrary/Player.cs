using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Text.RegularExpressions;


namespace DungeonLibrary;

public class Player : Entity {

	//=== FIELDS ===\\
	private int _killCount = 0;

	//=== PROPERTIES ===\\
	public int KillCount => _killCount;

	//=== CTOR ===\\
	public Player() :
		base("name",
		new Race(RaceType.Human),
		new Armour(ArmourType.Medium),
		new Weapon(WeaponType.Sword)) {
	}
	public Player(string name, Race race)
		: base(name, race, new Armour(ArmourType.Medium), new Weapon(WeaponType.Sword)) { }

	//=== METHODS ===\\
	public static Player CreateCharacter() {

		Console.Title = "CHARACTER CREATOR";

		//=== VARIABLES ===\\
		string nameChoice;
		RaceType rType;
		int charConfirm;

		do {
			Writer.Clear();
			Writer.WriteLine("YOUR CHARACTER");

			//=== NAME ===\\
			Writer.Write("\nName: ");
			int[] cursorPos = [Console.CursorLeft, Console.CursorTop];

			while(true) {
				Writer.CursorBottom();
				Writer.WriteLine("What would you like to name your character?\n");
				Writer.Write(">> ");
				nameChoice = Console.ReadLine();

				if(!string.IsNullOrEmpty(nameChoice.Trim()))
					break;
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

				for(int i = 0;i <= (int)RaceType.Orc;i++)
					Writer.WriteLine($"{i+1}) {(RaceType)i}");

				Writer.WriteLine();
				Writer.Write(">> ");

				input = Console.ReadLine().Trim().ToUpper();
				if(Regex.IsMatch(input, "^[1-5]$"))
					break;
			}

			rType = input switch {
				"1" => RaceType.Human,
				"2" => RaceType.Elf,
				"3" => RaceType.Dwarf,
				"4" => RaceType.Goblin,
				"5" => RaceType.Orc,
				_ => Race.RandomType()
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

	public void IncreaseScore(int score) => _score += score;

	public void ChangeArmour(Armour armour) {
		_score += (int)((armour.Defense - _armour.Defense) / _armour.Defense * 10);
		_score += (int)((armour.Dodge - _armour.Dodge) / _armour.Dodge * 10);
		_armour = armour;
	}

	public void ChangeWeapon(Weapon weapon) {
		_score += (int)((weapon.Damage - _weapon.Damage) / _weapon.Damage * 10);
		_score += (int)((weapon.Difficulty - _weapon.Difficulty) / _weapon.Difficulty * 10);
		_weapon = weapon;
	}

}

