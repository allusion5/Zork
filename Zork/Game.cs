using System;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
	public class Game
	{
		private static int Score { get; set; }
		private static int Moves { get; set; }
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
			Score = Moves = 0;
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
					case Commands.SCORE:
						Console.WriteLine($"Your score is {Score}, in {Moves} move(s).");
						break;
					case Commands.REWARD:
						Score++;
						break;
					default:
						Console.WriteLine($"Unrecognized command: {inputString.ToUpper()}");
						break;
				}
				Moves++;
				Score++;
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

