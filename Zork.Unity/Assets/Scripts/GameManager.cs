using UnityEngine;
using Zork.Common;
using TMPro;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string ZorkGameJsonFileAssetName = "Game";
    [SerializeField]
    private UnityOutputService OutputService;
    [SerializeField]
    private UnityInputService InputService;
    [SerializeField]
    private TextMeshProUGUI LocationText;
    [SerializeField]
    private TextMeshProUGUI MovesText;
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    private Game game;

    void Awake()
    {
        TextAsset gameJsonAsset =Resources.Load<TextAsset>(ZorkGameJsonFileAssetName);
        game = JsonConvert.DeserializeObject<Game>(gameJsonAsset.text);
        game.Run(InputService, OutputService);
    }
	void Start()
	{
        LocationText.text = game.Player.CurrentRoom.Name;
	}
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Return))
		{
            InputService.ProcessInput();
            LocationText.text = game.Player.CurrentRoom.Name;
            ScoreText.text = $"Score: {Game.Score}";
            MovesText.text = $"Moves: {Game.Moves}";
        }
        if (game.IsRunning == false)
		{
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
