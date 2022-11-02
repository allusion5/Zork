using System;


namespace Zork
{
	class Program
	{
		static void Main(string[] args)
		{

			string gameFilename = args.Length > 0 ? args[0] : @"Content\Game.json";

			Game game = Game.Load(gameFilename);
			Console.WriteLine("Welcome to Zork!");
			game.Run();
			Console.WriteLine("Thank you for playing!");
		}	
	}
}
