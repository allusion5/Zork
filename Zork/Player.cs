using System.Collections.Generic;
using Newtonsoft.Json;


namespace Zork
{
	public class Player
	{
		public World _world { get; }
		[JsonIgnore]
		public Room Location { get; private set; }
		[JsonIgnore]
		public string LocationName
		{
			get
			{
				return Location?.rName;
			}
			set
			{
				Location = _world?.RoomsByName.GetValueOrDefault(value);
			}
		}
		public Player (World world, string startingLocation)
		{
			_world = world;
			LocationName = startingLocation;
		}
		public bool Move(Directions direction)
		{
			bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
			if (isValidMove)
			{
				Location = destination;
			}
			return isValidMove;
		}
		public int Moves { get; }



	}
}