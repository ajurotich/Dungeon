using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public class Boss : Entity {

	//=== CTOR ===\\
	public Boss() 
		: base("dragonName", new Race(RaceType.Dragon), new Scales(), new Claws()) {}

}

internal class Scales : Armour {

	//=== CTOR ===\\
	internal Scales() : base(ArmourType.Heavy) {
		_mod = ArmourMod.Used;
	}

	//=== METHOD ===\\
	public override string ToString() => "SCALES";

}

internal class Claws : Weapon {

	//=== CTOR ===\\
	internal Claws() : base(WeaponType.Hammer) { 
		_mod = WeaponMod.Used;
	}

	//=== METHOD ===\\
	public override string ToString() => "CLAWS";

}
