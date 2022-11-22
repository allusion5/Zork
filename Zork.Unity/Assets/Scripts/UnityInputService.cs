using System;
using UnityEngine;
using Zork.Common;
using TMPro;

public class UnityInputService : MonoBehaviour, IInputService
{
	public event EventHandler<string> InputReceived;
	[SerializeField]
	private TMP_InputField InputField;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
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
