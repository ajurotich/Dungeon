using System;
using System.Text.Json;

namespace Common;

public class General {

	const int SCREENWIDTH = 80;

	public static void Header(string title) {
		if(title == "")
			return;

		Console.Title = title;
		Console.WriteLine("\n" + title);
		for(int i = 0;i<title.Length;i++)
			Console.Write("-");
		Console.WriteLine("\n");
	}

	public static void Header(string title, string description) {
		if(title == "")
			return;

		Console.Title = title;
		Console.WriteLine("\n" + title);
		Console.WriteLine(description);
		for(int i = 0;i<description.Length;i++)
			Console.Write("-");
		Console.WriteLine("\n");
	}

	public static void Footer() {
		Console.Clear();
		Console.WriteLine("\nThanks for playing!");
		Console.WriteLine("Created by Alias Jurotich");
		Border();
	}

	public static void Border() {
		Console.SetCursorPosition(0, 28);
		for(int i = 0; i<9; i++) Console.WriteLine(new string (' ', 80));

		Console.SetCursorPosition(0, 28);
		Console.WriteLine(new string('=', 80) + '\n');
    }

	public static void WaitForInput() {
		Border();
		Console.Write("\nPress any key to continue...");

		Console.ReadKey(true);
		Console.SetCursorPosition(0, Console.CursorTop);
		Console.WriteLine(new string(' ', 80));

		Console.SetCursorPosition(0, Console.CursorTop-4);

	}

	public static void Ellipsis(string message) {
		Console.Write(message);
		for(int i = 0;i < 3;i++) {
			Console.Write(".");
			System.Threading.Thread.Sleep(500);
		}
		Console.WriteLine();
	}

	public static void Stringify<T>(T input) =>
		JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });

	public static string Wrap(string v) {
		v = v.TrimStart();
		if(v.Length <= SCREENWIDTH)
			return v;

		var nextspace = v.LastIndexOf(' ', SCREENWIDTH);
		if(-1 == nextspace)
			nextspace = Math.Min(v.Length, SCREENWIDTH);

		return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
		"" : "\n" + Wrap(v.Substring(nextspace)));
	}


}
