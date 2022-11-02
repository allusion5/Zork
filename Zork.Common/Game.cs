using System;

namespace Zork.Common
{
	public class Game
	{
		private static int Score { get; set; }
		private static int Moves { get; set; }
		public World World { get; }
		public Player Player { get; }
		public IOutputService Output { get; private set; }

		public Game(World world, string startingLocation)
		{
			World = world;
			Player = new Player (World, startingLocation);
		}

		public void Run(IOutputService output)
		{
			Output = output;
			Room prevRoom = null;
			bool isRunning = true;
			Score = Moves = 0;
			while (isRunning)
			{
				Output.WriteLine(Player.CurrentRoom);
				if (prevRoom != Player.CurrentRoom)
				{
					Output.WriteLine(Player.CurrentRoom.Description);
					prevRoom = Player.CurrentRoom;
				}
				Output.Write("> ");
				string inputString = Console.ReadLine().Trim();
				char separator = ' ';
				string[] commandTokens = inputString.Split(separator);

				string verb = null;
				string subject = null;
				if (commandTokens.Length == 0)
				{
					continue;
				}
				else if (commandTokens.Length == 1)
				{
					verb = commandTokens[0];
				}
				else
				{
					verb = commandTokens[0];
					subject = commandTokens[1];
				}
				Commands command = ToCommand(verb);
				string outputString;
				switch (command)
				{
					case Commands.QUIT:
						isRunning = false;
						outputString = "Thank you for playing!";
						break;
					case Commands.LOOK:
						outputString = Player.CurrentRoom.Description;
						break;
					case Commands.NORTH:
					case Commands.SOUTH:
					case Commands.EAST:
					case Commands.WEST:
						Directions direction = (Directions)command;
						if (Player.Move(direction))
						{
							outputString = $"You moved {direction}.";
						}
						else
						{
							outputString = "The way is shut!";
						}
						break;
					case Commands.SCORE:
						outputString=$"Your score is {Score}, in {Moves} move(s).";
						break;
					case Commands.REWARD:
						Score++;
						outputString = null;
						break;
					case Commands.TAKE:
						//TODO
						outputString = null;
						break;
					case Commands.DROP:
						//TODO
						outputString = null;
						break;
					case Commands.INVENTORY:
						//TODO
						outputString = null;
						break;
					default:
						outputString=$"Unrecognized command: {inputString.ToUpper()}";
						break;
				}
				Output.WriteLine(outputString);
				Moves++;
				Score++;
			}
		}

		private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN;

	}
}
