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
				Console.Write("> ");
				string inputString = Console.ReadLine().Trim().ToUpper();
				Commands command = ToCommand(inputString);
				string outputString;
				switch (command)
				{
					case Commands.Quit:
						outputString = "Thank you for playing!";
						isRunning = false;
						break;
					case Commands.Look:
						outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
						break;
					case Commands.North:
					case Commands.South:
					case Commands.East:
					case Commands.West:
						outputString = $"You moved {inputString}.";
						break;
					default:
						outputString = $"Unrecognized command: {inputString}";
						break;
				}
				Console.WriteLine(outputString);
			}
		}
		static Commands ToCommand(string commandString)
		{
			if (Enum.TryParse<Commands>(commandString, true, out Commands command))
			{
				return command;
			}
			else
			{
				return Commands.Unknown;
			}
		}
	}
}
