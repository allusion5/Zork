using UnityEngine;
using Zork.Common;
using TMPro;
using System.Collections.Generic;

public class UnityOutputService : MonoBehaviour,IOutputService
{
    [SerializeField]
    private TextMeshProUGUI TextPrefab;
    [SerializeField]
    private Transform contentGUITransform;
    public void Write(object value) => ParseAndWriteLine(value.ToString());

    public void Write(string value) => ParseAndWriteLine(value.ToString());

    public void WriteLine(object value) => ParseAndWriteLine(value.ToString());

    public void WriteLine(string value) => ParseAndWriteLine(value.ToString());

    private void ParseAndWriteLine(string value)
	{
        var outputText = Instantiate(TextPrefab, contentGUITransform);
        outputText.text = value;
    }


}
