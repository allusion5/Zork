using System;
using System.Linq;
using Newtonsoft.Json;
using System.IO;


namespace Zork.Common
{
	public class Game
	{
		private static int Score { get; set; }
		private static int Moves { get; set; }
		public World World { get; }
		[JsonIgnore]
		public Player Player { get; }
		[JsonIgnore]
		public IInputService Input { get; private set; }
		[JsonIgnore]
		public IOutputService Output { get; private set; }
		[JsonIgnore]
		public bool IsRunning { get; private set; }
		public Game(World world, string startingLocation)
		{
			World = world;
			Player = new Player(World, startingLocation);
		}
		public static void Start(string gameFileName, IInputService input, IOutputService output)
		{
			Game game = JsonConvert.DeserializeObject<Game>(gameFileName);
			game.Run(input, output);
		}
		public void Run(IInputService input, IOutputService output)
		{
			Input = input ?? throw new ArgumentNullException(nameof(input));
			Output = output ?? throw new ArgumentNullException(nameof(output));

			IsRunning = true;
			Score = Moves = 0;
			Input.InputReceived += OnInputReceived;
			Output.WriteLine("Welcome to Zork!");
			Look();
		}

		public void OnInputReceived(object sender, string inputString)
		{
			char separator = ' ';
			string[] commandTokens = inputString.Split(separator);

			string verb;
			string subject = null;

			bool validMove = true;
			if (commandTokens.Length == 0)
			{
				return;
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
			switch (command)
			{
				case Commands.QUIT:
					IsRunning = false;
					break;
				case Commands.LOOK:
					Look();
					break;
				case Commands.NORTH:
				case Commands.SOUTH:
				case Commands.EAST:
				case Commands.WEST:
					Directions direction = (Directions)command;
					if (Player.Move(direction))
					{
						Output.WriteLine($"You moved {direction}.");
						Look();
					}
					else
					{
						Output.WriteLine("The way is shut!");
					}
					break;
				case Commands.SCORE:
					Output.WriteLine($"Your score is {Score}, in {Moves} move(s).");
					break;
				case Commands.REWARD:
					Score++;
					break;
				case Commands.TAKE:
					if (string.IsNullOrEmpty(subject))
					{
						Output.WriteLine("This command requires a subject.");
						validMove = false;
					}
					else
					{
						Take(subject);
					}
					break;
				case Commands.DROP:
					if (string.IsNullOrEmpty(subject))
					{
						Output.WriteLine("This command requires a subject.");
						validMove = false;
					}
					else
					{
						Drop(subject);
					}
					break;
				case Commands.INVENTORY:
					if (Player.Inventory.Count() == 0)
					{
						Output.WriteLine("You are empty handed.");
					}
					else
					{
						Output.WriteLine("You are carrying:");
						foreach (Item item in Player.Inventory)
						{
							Output.WriteLine(item.InventoryDescription);
						}
					}
					break;
				default:
					Output.WriteLine($"Unrecognized command: {inputString.ToUpper()}");
					validMove = false;
					break;
			}
			if (validMove)
			{
				Moves++;
			}
		}
		private void Look()
		{
			Output.WriteLine($"\n{ Player.CurrentRoom}");
			Output.WriteLine(Player.CurrentRoom.Description);
			foreach (Item item in Player.CurrentRoom.Inventory)
			{
				Output.WriteLine(item.LookDescription);
			}
		}
		private void Take(string itemName)
		{
			Item itemToTake = Player.CurrentRoom.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
			if (itemToTake == null)
			{
				Output.WriteLine("You can't see any such thing.");
			}
			else
			{
				Player.AddItemToInventory(itemToTake);
				Player.CurrentRoom.RemoveItemFromInventory(itemToTake);
				Output.WriteLine("Taken.");
			}
		}
		private void Drop(string itemName)
		{
			Item itemToDrop = Player.Inventory.FirstOrDefault(item => string.Compare(item.Name, itemName, ignoreCase: true) == 0);
			if (itemToDrop == null)
			{
				Output.WriteLine("You can't see any such thing.");
			}
			else
			{
				Player.CurrentRoom.AddItemToInventory(itemToDrop);
				Player.RemoveItemFromInventory(itemToDrop);
				Output.WriteLine("Dropped.");
			}
		}
		private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN;
	}

}
