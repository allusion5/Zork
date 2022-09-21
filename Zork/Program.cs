using System;

namespace Zork
{
	class Program
	{
		private static Room CurrentRoom
		{
			get { return _rooms[_currentRow, _currentColumn]; }
		}
		static void Main(string[] args)
		{
			InitRoomDescs();
			Console.WriteLine("Welcome to Zork!");
			bool isRunning = true;
			while (isRunning)
			{
				Console.WriteLine($"{CurrentRoom}");
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
						outputString =$"{CurrentRoom.Description}";
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
		private static void InitRoomDescs()
		{
			_rooms[0, 0].Description = "You are on a rock-strewn trail";
			_rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
			_rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall.";

			_rooms[1, 0].Description = "This is a forest, with trees in directions all around you.";
			_rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";
			_rooms[1, 2].Description = "You are behind a white house. In one corner of the house there is a small window which is slightly ajar.";

			_rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
			_rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
			_rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
		}
		private static readonly Room[,] _rooms =
		{
			{new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
			{new Room("Forest"), new Room("West of House"), new Room("Behind House")},
			{new Room("Dense Woods"), new Room("North of House"),new Room("Clearing") }
		};
		private static int _currentRow = 1;
		private static int _currentColumn = 1;
	}
}
