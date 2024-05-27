using System;
using System.Text.Json;

namespace Common;

public class General {

	//todo replace header with title
	public static void Header(string title) {
		if(title == "") return;

		Console.Title = title;
		Writer.CursorTop();
		Writer.WriteLine(title);
		for(int i = 0;i<title.Length;i++)
			Writer.Write("-");
		//Console.WriteLine("\n");
	}

	public static void Header(string title, string description) {
		if(title == "")
			return;

		Console.Title = title;
		Writer.WriteLine(title);
		Writer.WriteLine(description);
		for(int i = 0;i<description.Length;i++)
			Writer.Write("-");
		Writer.WriteLine();
		Writer.WriteLine();
	}

	public static void Footer() {
		Writer.Clear();
		Writer.CursorTop();
		Writer.WriteLine("\nThanks for playing!");
		Writer.WriteLine("Created by Alias Jurotich");
		Writer.CursorBottom();
	}

	public static void Stringify<T>(T input) =>
		JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });

	public static void WaitForInput() {
		Writer.CursorBottom();
		Writer.Write("Press any key to continue...");

		Console.ReadKey(true);
		Console.SetCursorPosition(0, Console.CursorTop);
		Console.WriteLine(new string(' ', 80));

		Console.SetCursorPosition(0, Console.CursorTop-4);

	}

}
