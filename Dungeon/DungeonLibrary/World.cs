using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DungeonLibrary;

public class World {

	//=== FIELDS ===\\
	private string _name;
	private string _description;
	private int _searchAmount;
	private Object[] _worldObjects;
	private bool _isSearched;

	//=== PROPS ===\\
	public string Name			=> _name;
	public string Description	=> _description;
	public int SearchAmount		=> _searchAmount;
	public Object[] Objects		=> _worldObjects;
	public bool IsSearched		=> _isSearched;

	//=== CTOR ===\\
	public World(bool isSearched) {
		_name = "";
		_description = "";
		_searchAmount = _isSearched ? 0 : 1;
		_isSearched = isSearched;
	}
	public World(string name, string description) {
		_name = name;
		_description = description;
		_searchAmount = new Random().Next(4,8);

		_worldObjects = new Object[_searchAmount];
		for(int i=0; i<_searchAmount; i++) {

			if(new Random().Next(1, 101) > 50) {
				RaceType rt = Race.RandomType();

				_worldObjects[i] = new Entity(
					RaceNames.GetRandomName(rt),
					new Race(rt),
					new Armour(Armour.RandomType()),
					new Weapon(Weapon.RandomType())
				);

			}
			else {
				_worldObjects[i] = (new Random().Next(1, 51)) switch {
					>0  and <=20 => new Armour(Armour.RandomType()),
					>20 and <=40 => new Weapon(Weapon.RandomType()),
					_ => new Potion(),
				};
			}

		}

		_isSearched = false;
	}

	//=== METHODS ===\\
	public void Display() {
		Writer.WriteLine(Name + "\n");
		Writer.WriteLine(Description + "\n");

		if(!IsSearched) {
			Console.CursorTop = 27;
			Writer.WriteLine("[!] There's more to discover here.");
		}
	}
	public void DisplayAllObjects() {
		Writer.WriteLine("\nWorlds Remaining Objects\n{");
		for(int i = 0; i < SearchAmount; i++)
			Writer.WriteLine("  " + Objects[i]);
		Writer.WriteLine(")\n");
	}
	public bool IncrementSearch() {
		_isSearched = (--_searchAmount == 0) ? true : false;
		return IsSearched;
	}

}
