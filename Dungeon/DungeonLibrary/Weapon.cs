using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public enum WeaponType {
	Dagger, //low damage, easy
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
	public WeaponType Type	=> _type;
	public WeaponMod Mod	=> _mod;
	public string Name		=> $"{Enum.GetName(_mod).ToLower()} {Enum.GetName(_type).ToLower()}";
	public float Damage		=> _damage;
	public float Difficulty	=> _difficulty;

	//=== CTOR ===\\
	public Weapon(WeaponType type) {
		_type = type;
		_mod = RandomMod();

		switch(_type) {
			case WeaponType.Dagger:
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

	public static WeaponMod RandomMod() {
		Random random = new Random();
		Array values = Enum.GetValues(typeof(WeaponMod));
		WeaponMod rType = (WeaponMod)values.GetValue(random.Next(values.Length));

		return rType;
	}

	public static void DisplayWeapon(Weapon w) {
		Console.WriteLine(w);
		for(int i = 0; i<w.Name.Length; i++) Console.Write("-");
		Console.WriteLine($"\nDamage:	{w.Damage}");
		Console.WriteLine($"Difficulty:	{w.Difficulty}\n");
	}

	public static void CompareWeapon(Weapon w1, Weapon w2) {
		if(w1.Name == w2.Name) return;

		Console.WriteLine("\n  COMPARE ARMOURS");
		Console.WriteLine("===================\n");

		Console.WriteLine($"Type:\t  " +
			$"{w1.ToString().PadLeft(12)}     " +
			$"{w2.ToString().PadRight(12)}");
		Console.WriteLine($"Damage:\t  " +
			$"{w1.Damage.ToString().PadLeft(12)}  " +
			$"{(w1.Damage>w2.Damage ? ">" : "<")}  " +
			$"{w2.Damage.ToString().PadRight(12)}");
		Console.WriteLine($"Difficulty:" +
			$"{w1.Difficulty.ToString().PadLeft(12)}  " +
			$"{(w1.Difficulty>w2.Difficulty ? ">" : "<")}  " +
			$"{w2.Difficulty.ToString().PadRight(12)}\n\n");

	}

	public override string ToString() => Name;

}
