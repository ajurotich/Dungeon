using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;

public enum TitleOptions {
	NAVIA,
	INFO,
	SEARCH,
	MOVE,
	FIGHT,
	QUIT,
}

public class Writer {

	//=== VARIABLES ===\\
	private const int SCREENWIDTH = 80;
	private const int SCREENHEIGHT = 42;
	private const int CURSORBORDER = 3;
	private const int TOPPOS = 7;
	private const int BOTTOMPOS = 31;
	private static int _topLine = 0;
	public static TitleOptions Title = TitleOptions.NAVIA;

	//=== FUNCTIONS ===\\
	public static void WindowSetup() {
		Console.Title = "THE DUNGEON OF NAVIA";
		Console.SetWindowSize(SCREENWIDTH, SCREENHEIGHT);
	   #pragma warning disable CA1416 // Validate platform compatibility
		Console.BufferWidth  = SCREENWIDTH;
		Console.BufferHeight = SCREENHEIGHT;
	   #pragma warning restore CA1416 // Validate platform compatibility
	}

	public static void Clear() {
		Console.Clear();
		Border();
		_topLine = 0;
	}

	private static void Border() => Border(0, SCREENHEIGHT);
	
	private static void Border(int startTop, int endTop) {
		//(int, int) cursorPos = Console.GetCursorPosition();
		//Console.WriteLine(new string(' ', SCREENWIDTH));
		//Console.SetCursorPosition(cursorPos.Item1, cursorPos.Item2);

		for(int y=Math.Max(0, startTop); y<=endTop && y<=SCREENHEIGHT-1; y++) {
			Console.CursorTop = y;
			Console.CursorLeft = 0;
			if(y==0 || y == TOPPOS-2 || y==BOTTOMPOS-2 || y==SCREENHEIGHT-1) {
				Console.Write("+");
				Console.Write(new string('-', SCREENWIDTH-2));
				Console.Write("+");
			}
			else {
				Console.Write("|");
				Console.Write(new string(' ', SCREENWIDTH-2));
				Console.Write("|");
			}
		}

		Console.SetCursorPosition(CURSORBORDER, TOPPOS);
		PageTitle();

	}

	public static void CursorTop() {
		Console.SetCursorPosition(CURSORBORDER, TOPPOS);

		Border(0, BOTTOMPOS-2);

		Console.SetCursorPosition(CURSORBORDER, TOPPOS);
	}

	public static void CursorBottom() {
		Console.SetCursorPosition(CURSORBORDER, BOTTOMPOS);

		Border(BOTTOMPOS, SCREENHEIGHT);

		Console.SetCursorPosition(CURSORBORDER, BOTTOMPOS);
	}

	//todo pagetitle
	public static void PageTitle() {
		int[] cursorPos = [Console.CursorLeft, Console.CursorTop];

		Console.SetCursorPosition(CURSORBORDER, 1);
		WriteLine(new string(' ', SCREENWIDTH-CURSORBORDER*2));
		WriteLine(new string(' ', SCREENWIDTH-CURSORBORDER*2));
		WriteLine(new string(' ', SCREENWIDTH-CURSORBORDER*2));
		Console.SetCursorPosition(CURSORBORDER, 1);

		switch(Title) {
			case TitleOptions.NAVIA: {
				WriteLine(" _          __       _         _       __ ");
				WriteLine("| |\\ |     / /\\     \\ \\  /    | |     / /\\ ");
				WriteLine("|_| \\|    /_/--\\     \\_\\/     |_|    /_/--\\ ");
				break;
			}
			case TitleOptions.INFO: {
				WriteLine(" _      _         ____     ___ ");
				WriteLine("| |    | |\\ |    | |_     / / \\ ");
				WriteLine("|_|    |_| \\|    |_|      \\_\\_/ ");
				break;
			}
			case TitleOptions.SEARCH: {
				WriteLine(" __      ____      __       ___      __       _    ");
				WriteLine("( (`    | |_      / /\\     | |_)    / /`     | |_| ");
				WriteLine("_)_)    |_|__    /_/--\\    |_| \\    \\_\\_,    |_| | ");
				break;
			}
			case TitleOptions.MOVE: {
				WriteLine(" _         ___      _         ____ ");
				WriteLine("| |\\/|    / / \\    \\ \\  /    | |_  ");
				WriteLine("|_|  |    \\_\\_/     \\_\\/     |_|__ ");
				break;
			}
			case TitleOptions.FIGHT: {
				WriteLine(" ____     _      __       _       _____ ");
				WriteLine("| |_     | |    / /`_    | |_|     | |  ");
				WriteLine("|_|      |_|    \\_\\_/    |_| |     |_|  ");
				break;
			}
			case TitleOptions.QUIT: {
				WriteLine(" ___       _        _     _____ ");
				WriteLine("/ / \\     | | |    | |     | |  ");
				WriteLine("\\_\\_\\\\    \\_\\_/    |_|     |_|  ");
				break;
			}
		}

		Console.SetCursorPosition(cursorPos[0], cursorPos[1]);
	}

	private static List<string> Wrap(string v) {
		const int WRAPSIZE = SCREENWIDTH - CURSORBORDER*2;
		List<string> output = [];

		int nextnewline = v.IndexOf('\n');

		if(v.Length <= WRAPSIZE) {
			if(nextnewline >= 1) {
				output.Add(v.Substring(0, nextnewline));
				output.AddRange(Wrap(v[(++nextnewline)..]));
				return output;
			}
			else {
				output.Add(v);
				return output;
			}
		}

		int nextspace = v.LastIndexOf(' ', WRAPSIZE);
		if(nextnewline >= 0 && nextnewline < nextspace) nextspace = nextnewline;

		if		(nextspace ==  0) nextspace = 1;
		else if (nextspace == -1) nextspace = Math.Min(v.Length, WRAPSIZE);

		output.Add(v.Substring(0, nextspace) /*+ ((nextspace >= v.Length) ? "" : "\n")*/);
		output.AddRange(Wrap(v[nextspace..]));

		return output;
	}

	public static void WriteLine(string value) {
		Console.CursorLeft = CURSORBORDER;
		List<string> valueList = Wrap(value);

		foreach(string s in valueList) { 
			Write(s);
			Console.CursorTop++;
			Console.CursorLeft = CURSORBORDER;
		}
		
	}

	public static void WriteLine() {
		Console.CursorTop++;
		Console.CursorLeft = CURSORBORDER;
	}
	
	public static void Write(string value) {
		if(string.IsNullOrEmpty(value)) return;

		List<string> valueList = Wrap(value);

		foreach(string s in valueList) {
			if(s.Contains('\n')) {
				Console.CursorTop++;
				Console.CursorLeft = CURSORBORDER;
				Console.Write(s.Replace("\n", ""));
			}
			else Console.Write(s);
		}
		
	}

	public static void Ellipsis(string message) {
		Writer.Write(message);
		for(int i = 0;i < 3;i++) {
			Writer.Write(".");
			System.Threading.Thread.Sleep(500);
		}
		Writer.WriteLine();
	}

}
