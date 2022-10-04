using System;
using System.IO;
using System.Collections.Generic;


namespace Zork
{
	class Program
	{
		static void Main(string[] args)
		{

			string gameFilename = args.Length > 0 ? args[0] : @"Content\Zork.json";

			Game game = Game.Load(gameFilename);
			Console.WriteLine("Welcome to Zork!");
			game.Run();
			Console.WriteLine("Thank you for playing!");
		}	
	}
}
