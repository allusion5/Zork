using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
	class Room
	{
		private string _rName;
		public string rName	{ get;set;}
		public string rDescription { get; set; }

		public Room(string name, string desc=null)
		{
			rName = name;
			rDescription = desc;
		}
		public override string ToString()
		{
			return rName;
		}
	}
}
