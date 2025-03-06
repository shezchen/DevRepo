using Unity.VisualScripting;
using UnityEngine;

public class GameStateControl : MonoBehaviour
{
    public static GameStateControl Instance { get; private set; }
    public string storyFile = "Assets/story/1.txt";
    public enum GameState
    {
        Menu,
        Game,
        Pause,
        Story
    }
    GameState _gameState=GameState.Menu;//默认主界面
    public void SetGameState(GameStateControl.GameState gameState)
    {
        _gameState = gameState;
        switch (_gameState)
        {
            case GameStateControl.GameState.Menu:
                break;
            case GameStateControl.GameState.Game:
                break;
            case GameStateControl.GameState.Pause:
                break;
            case GameStateControl.GameState.Story:
                StoryPlay();
                break;
            
        }
    }

    public void StoryPlay()
    {
        DialogPlayer.Instance.StartDialogPlay(storyFile);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the instance across scenes
        }
        else
        {
            Destroy(gameObject);
        }
      
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
