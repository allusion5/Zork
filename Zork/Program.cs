using System;

namespace Zork
{
	class Program
	{
		private static string CurrentRoom
		{
			get { return _rooms[_currentRow, _currentColumn]; }
		}
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Zork!");
			bool isRunning = true;
			while (isRunning)
			{
				Console.WriteLine(CurrentRoom);
				Console.Write("> ");
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
						outputString = $"Unrecognized command: {inputString.ToUpper()}";
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

		private static bool Move(Commands direction)
		{
			bool didMove = false;
			switch (direction)
			{
				case Commands.NORTH when _currentRow < _rooms.GetLength(0) - 1:
					_currentRow++;
					didMove = true;
					break;
				case Commands.SOUTH when _currentRow > 0:
					_currentRow--;
					didMove = true;
					break;
				case Commands.EAST when _currentColumn < _rooms.GetLength(1) - 1:
					_currentColumn++;
					didMove = true;
					break;
				case Commands.WEST when _currentColumn > 0:
					_currentColumn--;
					didMove = true;
					break;
			}
			return didMove;
		}
		private static readonly string[,] _rooms =
		{
			{"Rocky Trail", "South of House", "Canyon View" },
			{"Forest", "West of House", "Behind House"},
			{"Dense Woods", "North of House","Clearing" }
		};
		private static int _currentRow = 1;
		private static int _currentColumn = 1;
	}
}
