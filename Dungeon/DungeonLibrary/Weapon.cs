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
	public float Damage		=> MathF.Round(_damage, 1);
	public float Difficulty	=> MathF.Round(_difficulty, 1);

	//=== CTOR ===\\
	public Weapon(WeaponType type) {
		_type = type;
		_mod = RandomMod();

		switch(_type) {
			case WeaponType.Dagger:
				_damage		= 15;
				_difficulty = 2;
				break;
			case WeaponType.Sword:
				_damage		= 20;
				_difficulty = 4;
				break;
			case WeaponType.Axe:
				_damage		= 25;
				_difficulty = 6;
				break;
			case WeaponType.Hammer:
				_damage		= 30;
				_difficulty = 8;
				break;
		}
		switch(_mod) {
			case WeaponMod.Epic:
				_damage		+= 5;
				_difficulty	-= 2;
				break;
			case WeaponMod.New:
				_damage     += 2.5f;
				_difficulty -= 1;
				break;
			case WeaponMod.Used:
				break;
			case WeaponMod.Old:
				_damage     -= 2.5f;
				_difficulty += 1;
				break;
			case WeaponMod.Broken:
				_damage     -= 5;
				_difficulty += 2;
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

		Console.WriteLine("\n  COMPARE WEAPONS");
		Console.WriteLine(  "===================\n");

		Console.WriteLine	($"Type:	    " +
			$"{w1.ToString().PadLeft(10)}   " +
			$"{w2.ToString().PadRight(10)}");
		Console.WriteLine(	$"Damage:	   " +
			$"{w1.Damage.ToString().PadLeft(10)}  " +
			$"{(w1.Damage==w2.Damage ? "=" : (w1.Damage>w2.Damage ? ">" : "<"))}  " +
			$"{w2.Damage.ToString().PadRight(10)}");
		Console.WriteLine(	$"Difficulty:" +
			$"{w1.Difficulty.ToString().PadLeft(10)}  " +
			$"{(w1.Difficulty==w2.Difficulty ? "=" : (w1.Difficulty>w2.Difficulty ? ">" : "<"))}  " +
			$"{w2.Difficulty.ToString().PadRight(10)}\n\n");

	}

	public override string ToString() => Name;

}
