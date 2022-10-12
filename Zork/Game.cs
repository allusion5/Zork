using System;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
	public class Game
	{
		public World _world { get; set; }
		[JsonIgnore]
		public Player _player { get; set; }
		[JsonIgnore]
		private bool isRunning { get; set; }

		public Game(World world, Player player)
		{
			_world = world;
			_player = player;
		}

		public void Run()
		{
			isRunning = true;
			Room prevRoom = null;
			while (isRunning)
			{
				Console.WriteLine($"{_player.Location}");
				if (prevRoom != _player.Location)
				{
					Console.WriteLine(_player.Location.Description);
					prevRoom = _player.Location;
				}
				Console.Write("> ");
				string inputString = Console.ReadLine().Trim();
				Commands command = ToCommand(inputString);

				switch (command)
				{
					case Commands.QUIT:
						isRunning = false;
						break;
					case Commands.LOOK:
						Console.WriteLine(_player.Location.Description);
						break;
					case Commands.NORTH:
					case Commands.SOUTH:
					case Commands.EAST:
					case Commands.WEST:
						Directions direction = Enum.Parse<Directions>(command.ToString(), true);
						if (_player.Move(direction) == false)
						{
							Console.WriteLine("The way is shut!");
						}
						break;
					default:
						Console.WriteLine($"Unrecognized command: {inputString.ToUpper()}");
						break;
				}
			}
		}

		public static Game Load(string filename)
		{
			Game _game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
			_game._player = _game._world.SpawnPlayer();
			return _game;
		}


		static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN;
	}
}

