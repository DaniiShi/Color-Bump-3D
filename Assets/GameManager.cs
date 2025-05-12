using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { Playing, NotPlaying }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState currentState;

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
        SetGameState(GameState.NotPlaying);
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;
    }

    public GameState CurrentState => currentState;
}
