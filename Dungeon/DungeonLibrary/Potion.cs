using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public enum Type {
	Lesser,
	Greater,
	Full
}

internal class Potion {

	//=== FIELDS ===\\
	private Type _type;
	private float _healPercent;

	//=== PROPS ===\\
	public Type Type			=> _type;
	public float HealPercent	=> _healPercent;

	//=== CTOR ===\\
	public Potion() {
		_healPercent = new Random().Next(10, 41);
		if(_healPercent != 100) _healPercent = 100;

		_type = _healPercent switch {
			100		=> Type.Full,
			>=30	=> Type.Greater,
			_		=> Type.Lesser
		};
	}

	//=== METHODS ===\\

}
