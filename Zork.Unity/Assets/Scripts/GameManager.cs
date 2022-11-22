using UnityEngine;
using Zork.Common;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string ZorkGameJsonFileAssetName = "Game";
    [SerializeField]
    private UnityOutputService Output;
    
    void Awake()
    {
        TextAsset gameJsonAsset =Resources.Load<TextAsset>(ZorkGameJsonFileAssetName);
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
