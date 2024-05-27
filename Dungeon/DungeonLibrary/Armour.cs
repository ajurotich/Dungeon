using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

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
	public float Dodge		=> MathF.Round(_dodge, 1);
	public float Defense	=> MathF.Round(_defense, 1);

	//=== CTOR ===\\
	public Armour(ArmourType type) {
		_type = type;
		_mod = RandomMod();

		switch (_type) {
			case ArmourType.Light:
				_dodge		= 15;
				_defense	= 5;
				break;
			case ArmourType.Medium:
				_dodge		= 10;
				_defense	= 10;
				break;
			case ArmourType.Heavy:
				_dodge		= 5;
				_defense	= 15;
				break;
		}
		switch (_mod) {
			case ArmourMod.Epic:
				_defense	+= 2f;
				_dodge		+= 2f;
				break;
			case ArmourMod.New:
				_defense    += 1f;
				_dodge      += 1f;
				break;
			case ArmourMod.Used:
				break;
			case ArmourMod.Old:
				_defense    -= 1f;
				_dodge      -= 1f;
				break;
			case ArmourMod.Broken:
				_defense    -= 2f;
				_dodge      -= 2f;
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
		Writer.WriteLine(a.ToString());
		for(int i = 0; i<a.Name.Length; i++) Writer.Write("-");
		Writer.WriteLine($"\nDefense:	{a.Defense}\n");
		Writer.WriteLine($"Dodge:		{a.Dodge}");
	}

	public static void CompareArmour(Armour a1, Armour a2) {
		if(a1.Name == a2.Name) return;

		Writer.WriteLine("\n  COMPARE ARMOURS");
		Writer.WriteLine(  "===================\n");

		int padSize = 13;
		Writer.WriteLine($"Type:    " +
			$"{a1.ToString().PadLeft(padSize)}   " +
			$"{a2.ToString().PadRight(padSize)}");
		Writer.WriteLine($"Defense:"  +
			$"{a1.Defense.ToString().PadLeft(padSize)}  " +
			$"{(a1.Defense==a2.Defense ? "=" : (a1.Defense>a2.Defense ? ">" : "<"))}  " +
			$"{a2.Defense.ToString().PadRight(padSize)}");
		Writer.WriteLine($"Dodge:  " +
			$"{a1.Dodge.ToString().PadLeft(padSize)}  " +
			$"{(a1.Dodge==a2.Dodge ? "=" : (a1.Dodge>a2.Dodge ? ">" : "<"))}  " +
			$"{a2.Dodge.ToString().PadRight(padSize)}\n\n");
	}

	public override string ToString() => Name;

}
