using System;
using System.Text.Json;

namespace DungeonLibrary {
	public class Signature {

		public static void Header(string title) {
			if(title == "") return;

			Console.Title = title;
			Console.WriteLine("\n" + title);
			for(int i = 0;i<title.Length;i++) Console.Write("-");
			Console.WriteLine("\n");
		}

		public static void Header(string title, string description) {
			if(title == "") return;

			Console.Title = title;
			Console.WriteLine("\n" + title);
			Console.WriteLine(description);
			for(int i = 0;i<description.Length;i++) Console.Write("-");
			Console.WriteLine("\n");
		}

		public static void Stringify<T>(T input) {
			Console.WriteLine(JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true }));
		}

		public static void Footer() {
			Console.Clear();
			Console.WriteLine("\n\n\n\n");
			Console.WriteLine("Thanks for playing!");
			Console.WriteLine("Created by Alias Jurotich");
			Console.WriteLine("\n\n\n\n");
		}

	}
}
