using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 lastInputPosition = Vector3.zero;

    public float sensitivity = 0.16f, clampDelta = 42f;
    public GameManager gameManager;
    private bool gameStarted = false;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Ожидаем тапа, если игра ещё не началась
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            gameManager.SetGameState(GameState.Playing);
            gameStarted = true;
        }

        // Если игра ещё не началась — не обрабатывать движение
        if (!gameStarted || gameManager.CurrentState == GameState.NotPlaying)
            return;

        HandleInput();
    }

    private void HandleInput()
    {
        // Обработка ввода для WebGL (мышь и сенсорный ввод)
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            lastInputPosition = GetInputPosition();
        }

        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector3 currentInputPosition = GetInputPosition();
            Vector3 vector = lastInputPosition - currentInputPosition;
            lastInputPosition = currentInputPosition;
            vector = new Vector3(vector.x, 0, vector.y);

            Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);
            rb.AddForce(-moveForce * sensitivity - rb.linearVelocity / 5f, ForceMode.VelocityChange);
        }
    }

    private Vector3 GetInputPosition()
    {
        // Возвращает позицию мыши или касания
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return Input.mousePosition;
        }
    }
}