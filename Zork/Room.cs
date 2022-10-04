using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace Zork
{
	public class Room
	{
		[JsonProperty(Order = 1)]
		public string rName	{ get;set;}
		[JsonProperty(Order = 2)]
		public string rDescription { get; set; }
		[JsonProperty(PropertyName = "Neighbors",Order = 3)]
		private Dictionary<Directions, string> NeighborNames { get; set; }

		[JsonIgnore]
		public IReadOnlyDictionary<Directions, Room> Neighbors { get; private set; }
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
			return lhs.rName==rhs.rName;
		}
		public static bool operator !=(Room lhs, Room rhs) => !(lhs == rhs);
		public override bool Equals(object obj) => obj is Room room ? this == room : false;
		public bool Equal(Room other) => this == other;
		public override string ToString() => rName;
		public override int GetHashCode()=>rName.GetHashCode();
		public void UpdateNeighbors(World world)=>Neighbors = (from entry in NeighborNames
															   let room = world.RoomsByName.GetValueOrDefault(entry.Value)
															   where room!= null
															   select (Direction: entry.Key, Room: room)).ToDictionary(pair=>pair.Direction,pair=>pair.Room);


	}
}
