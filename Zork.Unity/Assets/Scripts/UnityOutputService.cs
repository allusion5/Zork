using System;
using UnityEngine;
using Zork.Common;
using TMPro;

public class UnityOutputService : MonoBehaviour,IOutputService
{
    public void Write(object obj)
    {
        Console.Write(obj);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(object obj)
    {
        Console.WriteLine(obj);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
    [SerializeField]
    private TextMeshProUGUI OutputText;
}
