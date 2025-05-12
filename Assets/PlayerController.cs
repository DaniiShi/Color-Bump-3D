using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    private float forwardSpeed = 10f;        // Скорость по Z

    private Rigidbody rb;

    private Camera mainCamera;

    private Vector3 lastInputPosition = Vector3.zero;

    public float sensitivity = .16f, clampDelta = 42f;
    public float bounds = 5;





    public GameManager gameManager;


    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (gameManager.CurrentState == GameState.NotPlaying) return;
        HandleInput();
    }

    void FixedUpdate()
    {
        if (gameManager.CurrentState == GameState.NotPlaying) return;
        MovePlayer();
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

    private void MovePlayer()
    {
        // Текущая позиция
        Vector3 pos = rb.position;

        // Двигаемся вперёд по Z всегда
        pos.z += forwardSpeed * Time.fixedDeltaTime;


        // // Назад по Z не даём скатиться (если нужно)
        // pos.z = Mathf.Max(pos.z, mainCamera.transform.position.z + 3); // Опционально

        // Обновляем позицию
        rb.MovePosition(pos);
    }

    public void SetSpeed(float speed)
    {
        forwardSpeed = speed;
    }
}
