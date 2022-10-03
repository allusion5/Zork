using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork
{
	class Program
	{
		private static Room[,] _rooms;
		private static Room CurrentRoom
		{
			get { return _rooms[_currentRow, _currentColumn]; }
		}
		static void Main(string[] args)
		{
			string roomsFilename = args.Length > 0?args[0]:@"Content\Rooms.json";
			InitRooms(roomsFilename);
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
		private static void InitRooms(string roomsFilename)=>
			_rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));

		private enum Fields
		{
			rName=0,
			rDescription
		}
		private static int _currentRow = 1;
		private static int _currentColumn = 1;
	}
}
