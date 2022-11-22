using System;
using Zork.Common;

namespace Zork.Cli
{
	internal class ConsoleInputService : IInputService
	{
		public event EventHandler<string> InputReceived;

		public void ProcessInput()
		{
			string inputString = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(inputString)==false)
			{
				InputReceived?.Invoke(this, inputString);
			}
		}
	}
}
