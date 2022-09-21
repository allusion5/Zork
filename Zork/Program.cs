using System;
using System.Collections.Generic;

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
						outputString = $"{CurrentRoom.rDescription}";
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
			var roomMap = new Dictionary<string, Room>();
			foreach(Room room in _rooms)
			{
				roomMap.Add(room.rName, room);
			}

			roomMap["Rocky Trail"].rDescription = "You are on a rock-strewn trail.";
			roomMap["South of House"].rDescription = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
			roomMap["Canyon View"].rDescription = "You are at the top of the Great Canyon on its south wall.";

			roomMap["Forest"].rDescription = "This is a forest, with trees in directions all around you.";
			roomMap["West of House"].rDescription = "This is an open field west of a white house, with a boarded front door.";
			roomMap["Behind House"].rDescription = "You are behind a white house. In one corner of the house there is a small window which is slightly ajar.";

			roomMap["Dense Woods"].rDescription = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
			roomMap["North of House"].rDescription = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
			roomMap["Clearing"].rDescription = "You are in a clearing, with a forest surrounding you on the west and south.";
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
