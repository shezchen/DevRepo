using Unity.VisualScripting;
using UnityEngine;

public class GameStateControl : MonoBehaviour
{
    public static GameStateControl Instance { get; private set; }

    public enum GameState
    {
        Menu,
        Game,
        Pause,
        Story
    }
    GameState _gameState=GameState.Menu;
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
                DialogPlayer.Instance.StartDialogPlay("Assets/story/1.txt");
                break;
            
        }
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
