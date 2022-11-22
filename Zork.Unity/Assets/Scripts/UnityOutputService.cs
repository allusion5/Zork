using System;
using UnityEngine;
using Zork.Common;
using TMPro;

public class UnityOutputService : MonoBehaviour,IOutputService
{
    public void Write(object value) => Write(value.ToString());

    public void Write(string value)
    {
        OutputText.text = value;
    }

    public void WriteLine(object value) => WriteLine(value.ToString());

    public void WriteLine(string value)
    {
        OutputText.text = value;
        Instantiate(OutputText, parent.transform);
    }
    [SerializeField]
    private TextMeshProUGUI OutputText;
    [SerializeField]
    private GameObject parent;
}
