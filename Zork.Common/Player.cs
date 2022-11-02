﻿using System;
using System.Collections.Generic;

namespace Zork.Common
{
	public class Player
	{
		public Room CurrentRoom
		{
			get => _currentRoom;
			set => _currentRoom = value;
		}
		public List<Item> Inventory { get; }
		public Player(World world, string startingLocation)
		{
			_world = world;

			if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
			{
				throw new Exception($"Invalid starting location: {startingLocation}");
			}

			Inventory = new List<Item>();
		}
		private World _world;
		private Room _currentRoom;
		public bool Move(Directions direction)
		{
			bool isValid = _currentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
			if (isValid)
			{
				CurrentRoom = neighbor;
			}
			return isValid;
		}
	}
}
