using System;
using UnityEngine;
using Zork.Common;
using TMPro;

public class UnityInputService : MonoBehaviour, IInputService
{
	public event EventHandler<string> InputReceived;
	[SerializeField]
	private TMP_InputField InputField;

	public void ProcessInput()
	{
		{
			string inputString = InputField.text;
			if (string.IsNullOrWhiteSpace(inputString) == false)
			{
				InputReceived?.Invoke(this, inputString);
			}
			InputField.text = string.Empty;
		}
	}
}
