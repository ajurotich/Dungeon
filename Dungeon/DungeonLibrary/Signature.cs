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

		public static string Wrap(string v, int size) {
			v = v.TrimStart();
			if(v.Length <= size)
				return v;
			var nextspace = v.LastIndexOf(' ', size);
			if(-1 == nextspace)
				nextspace = Math.Min(v.Length, size);
			return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
			"" : "\n" + Wrap(v.Substring(nextspace), size));
		}

		public static void Footer() {
			Console.Clear();
			Console.WriteLine("\nThanks for playing!");
			Console.WriteLine("Created by Alias Jurotich");
			Console.WriteLine("\n\n\n\n");
		}

	}
}
