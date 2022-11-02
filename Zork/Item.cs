using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Zork
{
	class Item
	{
		[JsonProperty(PropertyName = "ItemName", Order = 1)]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "WorldDesc", Order = 2)]
		public string WorldDescription { get; set; }
		[JsonProperty(PropertyName = "InventoryDesc", Order = 3)]
		public string InventoryDescription { get; set; }

		public Item(string name, string worldDesc, string invDesc)
		{
			Name = name;
			WorldDescription = worldDesc;
			InventoryDescription = invDesc;
		}
	}
}
