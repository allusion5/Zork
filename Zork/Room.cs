using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Zork
{
	public class Room
	{
		[JsonProperty(Order = 1)]
		public string Name	{ get;set;}
		[JsonProperty(Order = 2)]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "Neighbors",Order = 3)]
		private Dictionary<Directions, string> NeighborNames { get; set; }
		[JsonIgnore]
		public IReadOnlyDictionary<Directions, Room> Neighbors { get; private set; }
		[JsonProperty(PropertyName = "InventoryNames", Order = 4)]
		private  { get; set; }

	public Room(string name, string description, Dictionary<Directions, string> neighborNames, string InventoryNames)
		{
			Name = name;
			Description = description;
			NeighborNames = neighborNames;


		}
		public static bool operator ==(Room lhs, Room rhs)
		{
			if (ReferenceEquals(lhs, rhs))
			{
				return true;
			}
			if(lhs is null || rhs is null)
			{
				return false;
			}
			return lhs.Name == rhs.Name;
		}
		public static bool operator !=(Room lhs, Room rhs) => !(lhs == rhs);
		public override bool Equals(object obj) => obj is Room room ? this == room : false;
		public bool Equal(Room other) => this == other;
		public override string ToString() => Name;
		public override int GetHashCode()=> Name.GetHashCode();
		public void UpdateNeighbors(World world)=>Neighbors = (from entry in NeighborNames
															   let room = world.RoomsByName.GetValueOrDefault(entry.Value)
															   where room!= null
															   select (Direction: entry.Key, Room: room)).ToDictionary(pair=>pair.Direction,pair=>pair.Room);
		public void UpdateInventory (World world)
		{
			Inventory = new 
			InventoryNames = null;
		}


	}
}
