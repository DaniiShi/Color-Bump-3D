using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { Playing, NotPlaying }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState currentState;

    public int currentLevelIndex = 0;
    [HideInInspector]
    public LevelDifficulty currentLevelDifficulty;

    public ColorPalette currentColorPalette;

    public UIManager uiManager;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 0f;
        uiManager.StartState();
        SetGameState(GameState.NotPlaying);
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;
    }

    public void Finish()
    {
        SetGameState(GameState.NotPlaying);
        // Замедление времени
        Time.timeScale = 0.5f;
        uiManager.FinishState();
    }

    public GameState CurrentState => currentState;
}
