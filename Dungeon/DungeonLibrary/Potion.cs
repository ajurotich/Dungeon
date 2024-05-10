using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary;

public enum Descriptor {
	invincible,
	fearless,
	intoxicating,
	mysterious,
	enchanting,
	vigorous,
	diligent,
	overwhelming,
	gorgeous,
	passionate,
	terrifying,
	beautiful,
	powerful,
	sexy
}
public enum Verb {
	absorb,
	eat,
	swallow,
	drain,
	gulp,
	guzzle,
	inhale,
	sip,
	slurp,
	suck,
	down,
	drink
}

public class Potion {

	//=== FIELDS ===\\
	private Descriptor _descriptor;
	private Verb _verb;
	private float _healPercent;

	//=== PROPS ===\\
	public string Descriptor	=> Enum.GetName(_descriptor);
	public string Verb			=> Enum.GetName(_verb);
	public float HealPercent	=> _healPercent;

	//=== CTOR ===\\
	public Potion() {
		Random rand = new Random();

		Array values = Enum.GetValues(typeof(Descriptor));
		_descriptor = (Descriptor)values.GetValue(rand.Next(values.Length));

		values = Enum.GetValues(typeof(Verb));
		_verb = (Verb)values.GetValue(rand.Next(values.Length));

		_healPercent = (rand.Next(0, 101) == 100) ? 100 : rand.Next(10, 41);

	}

	//=== METHODS ===\\
	
}
