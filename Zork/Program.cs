using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
	class Program
	{
		private static readonly Dictionary<string, Room> roomMap;

		static Program()
		{
			roomMap = new Dictionary<string, Room>();
			foreach (Room room in _rooms)
			{
				roomMap[room.rName]=room;
			}
		}
		private static Room CurrentRoom
		{
			get { return _rooms[_currentRow, _currentColumn]; }
		}
		static void Main(string[] args)
		{
			string roomsFilename = args.Length > 0?args[0]:@"Content\Rooms.txt";
			InitRoomDescs(roomsFilename);
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
		private static void InitRoomDescs(string roomsFilename)
		{
			const string fieldDelimiter = "##";
			const int expectedFieldCount = 2;
			var roomQuery = from line in File.ReadLines(roomsFilename)
							let fields = line.Split(fieldDelimiter)
							where fields.Length == expectedFieldCount
							select (Name: fields[(int)Fields.Name],
							Description: fields[(int)Fields.Description]);
			foreach(var(Name,Description) in roomQuery)
			{
				roomMap[Name].rDescription = Description;
			}

		}
		private enum Fields
		{
			Name=0,
			Description
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
