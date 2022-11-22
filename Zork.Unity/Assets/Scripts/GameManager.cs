using UnityEngine;
using Zork.Common;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string ZorkGameJsonFileAssetName = "Game";
    [SerializeField]
    private UnityOutputService OutputService;
    [SerializeField]
    private UnityInputService InputService;
    
    void Awake()
    {
        TextAsset gameJsonAsset =Resources.Load<TextAsset>(ZorkGameJsonFileAssetName);
        Game.Start(gameJsonAsset.text, InputService, OutputService);
    }
}
