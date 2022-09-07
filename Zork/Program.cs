using System;

namespace Zork
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Zork!");
			bool isRunning = true;
			while (isRunning)
			{
				Console.WriteLine($"{_rooms[_currentRoom]}\n> ");
				string inputString = Console.ReadLine().Trim();
				Commands command = ToCommand(inputString);
				string outputString;
				switch (command)
				{
					case Commands.QUIT:
						outputString = "Thank you for playing!";
						isRunning = false;
						break;
					case Commands.LOOK:
						outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
						break;
					case Commands.NORTH:
					case Commands.SOUTH:
					case Commands.EAST:
					case Commands.WEST:
						if (Move(command))
						{
							outputString = $"You moved {command}.";
						}
						else
						{
							outputString = "The way is shut!";
						}
						break;
					default:
						outputString = $"Unrecognized command: {command}";
						break;
				}
				Console.WriteLine(outputString);
			}
		}
		static Commands ToCommand(string commandString)
		{
			if (Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands command))
			{
				return command;
			}
			else
			{
				return Commands.UNKNOWN;
			}
		}

		private static bool Move(Commands command)
		{
			bool didMove=false;
			switch (command)
			{
				case Commands.NORTH:
				case Commands.SOUTH:
					break;
				case Commands.EAST when _currentRoom < _rooms.Length - 1:
					_currentRoom++;
					didMove = true;
					break;
				case Commands.WEST when _currentRoom > 0:
					_currentRoom--;
					didMove = true;
					break;
			}
			return didMove;
		}
		private static readonly string[] _rooms = { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
		private static int _currentRoom = 1;
	}
}
