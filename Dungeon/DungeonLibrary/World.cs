using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

//https://chat.openai.com/share/8ce25086-b521-4c56-a4ad-2120598d5944

public class World {

	//=== FIELDS ===\\
	private string _name;
	private string _description;
	private int _searchAmount;
	private bool _isSearched;

	//=== PROPS ===\\
	public string Name			=> _name;
	public string Description	=> _description;
	public int SearchAmount		=> _searchAmount;
	public bool IsSearched		=> _isSearched;

	//=== CTOR ===\\
	public World(bool isSearched) {
		_name = "";
		_description = "";
		_isSearched = isSearched;
	}
	public World(string name, string description, int searchAmount) {
		_name = name;
		_description = description;
		_searchAmount = searchAmount;
		_isSearched = false;
	}

	//=== METHODS ===\\
	public void Display() {
		Console.WriteLine(Name + "\n");
		Console.WriteLine(Signature.Wrap(Description, 80) + "\n");
		if(!IsSearched) Console.WriteLine("There's more to discover here.");
	}
	public bool IncrementSearch() {
		_isSearched = (--_searchAmount == 0) ? true : false;
		return IsSearched;
	}

}
