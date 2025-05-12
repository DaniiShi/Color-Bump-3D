using UnityEngine;

public class StartByTap : MonoBehaviour
{
    public GameManager gameManager;
    private bool gameStarted = false;
    void Start()
    {
        Time.timeScale = 0f; // Останавливаем игру

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameManager.SetGameState(GameState.Playing);
            Time.timeScale = 1f; // Запускаем игру
            gameStarted = true;
        }
    }
}
