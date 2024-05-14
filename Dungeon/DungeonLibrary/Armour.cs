using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

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
	private float _dodge, _defense;

	//=== PROPS ===\\
	public ArmourType Type	=> _type;
	public ArmourMod Mod	=> _mod;
	public string Name		=> $"{Enum.GetName(_mod).ToLower()} {Enum.GetName(_type).ToLower()}";
	public float Dodge		=> MathF.Round(_dodge, 2);
	public float Defense	=> MathF.Round(_defense, 2);

	//=== CTOR ===\\
	public Armour(ArmourType type) {
		_type = type;
		_mod = RandomMod();

		switch (_type) {
			case ArmourType.Light:
				_dodge = .3f;
				_defense = .2f;
				break;
			case ArmourType.Medium:
				_dodge = .2f;
				_defense = .4f;
				break;
			case ArmourType.Heavy:
				_dodge = .1f;
				_defense = .6f;
				break;
		}
		switch (_mod) {
			case ArmourMod.Epic:
				_defense *= 1.15f;
				break;
			case ArmourMod.New:
				_defense *= 1.05f;
				break;
			case ArmourMod.Used:
				break;
			case ArmourMod.Old:
				_defense *= .95f;
				break;
			case ArmourMod.Broken:
				_defense *= .85f;
				break;
		}
	}

	//=== METHODS ===\\
	public static ArmourType RandomType() {
		Random random = new Random();
		Array values = Enum.GetValues(typeof(ArmourType));
		ArmourType rType = (ArmourType)values.GetValue(random.Next(values.Length));

		return rType;
	}

	public static ArmourMod RandomMod() {
		Random random = new Random();
		Array values = Enum.GetValues(typeof(ArmourMod));
		ArmourMod rType = (ArmourMod)values.GetValue(random.Next(values.Length));

		return rType;
	}

	public static void DisplayArmour(Armour a) {
		Console.WriteLine(a);
		for(int i = 0; i<a.Name.Length; i++) Console.Write("-");
		Console.WriteLine($"\nDefense:	{a.Defense}\n");
		Console.WriteLine($"Dodge:		{a.Dodge}");
	}

	public static void CompareArmour(Armour a1, Armour a2) {
		if(a1.Name == a2.Name) return;

		Console.WriteLine("\n  COMPARE ARMOURS");
		Console.WriteLine("===================\n");

		Console.WriteLine($"Type:\t " +
			$"{a1.ToString().PadLeft(12)}   " +
			$"{a2.ToString().PadRight(12)}");
		Console.WriteLine($"Defense:" +
			$"{a1.Defense.ToString().PadLeft(12)}  " +
			$"{(a1.Defense==a2.Defense ? "=" : (a1.Defense>a2.Defense ? ">" : "<"))}  " +
			$"{a2.Defense.ToString().PadRight(12)}");
		Console.WriteLine($"Dodge:\t" +
			$"{a1.Dodge.ToString().PadLeft(12)}  " +
			$"{(a1.Dodge==a2.Dodge ? "=" : (a1.Dodge>a2.Dodge ? ">" : "<"))}  " +
			$"{a2.Dodge.ToString().PadRight(12)}\n\n");
	}

	public override string ToString() => Name;

}
