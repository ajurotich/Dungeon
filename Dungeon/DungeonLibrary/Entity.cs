using Common;
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
	protected int _score;

	//=== PROPERTIES ===\\
	public string Name	 => _name.ToUpper();
	public float Health	 => MathF.Round(_health, 1);
	public bool IsAlive	 => _isAlive;
	public Race Race	 => _race;
	public Armour Armour => _armour;
	public Weapon Weapon => _weapon;
	public int Score	 => _score;

	//=== CTOR ===\\
	public Entity(string name, Race race, Armour armour, Weapon weapon) {
		_name = name;

		_race = race;
		_health = race.MaxHealth;
		_isAlive = true;

		_armour = armour;
		_weapon = weapon;

		_score = (int)((Health + Armour.Defense + Weapon.Damage)
				 * (new Random().NextSingle() *.25f + .775f));
	}

	//=== METHODS ===\\
	public void Damage(float damageAmount) {
		if(damageAmount <= 0) return;

		_health -= MathF.Round(damageAmount, 1);
		_score -= (int)(damageAmount * .15f);

		if(Health <= 0) _isAlive = false;
	}

	public void Heal(float healAmount) {
		_health = Math.Clamp((_health += MathF.Round(healAmount, 1)), 0, Race.MaxHealth);
		_score += (int)((_health - healAmount) / _health * 10);
	}

	public void Display() {
		for(int i= 0; i < 73; i++) Writer.Write("=");

		Writer.WriteLine($"\n  Displaying \'{Name}\' the {Enum.GetName(Race.Type)}:\n");

		int p = 16;

		Writer.WriteLine($"\t{("Health:").PadRight(p)}{Health}/{Race.MaxHealth}");
		Writer.WriteLine($"\t{("Armour:").PadRight(p)}{Armour.ToString()}\n" +
						 $"\t {("-Defense:").PadRight(p)}{Armour.Defense}\n" +
						 $"\t {("-Dodge:").PadRight(p)}{Armour.Dodge}");
		Writer.WriteLine($"\t{("Weapon:").PadRight(p)}{Weapon.ToString()}\n" +
						 $"\t {("-Damage:").PadRight(p)}{Weapon.Damage}\n" +
						 $"\t {("-Difficulty:").PadRight(p)}{Weapon.Difficulty}");

		for(int i= 0; i < 72; i++) Writer.Write("=");
		Writer.WriteLine("\n");
	}
	
}
