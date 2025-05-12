using UnityEngine;

public class MainObjMovement : MonoBehaviour
{
    private float speed;
    public GameManager gameManager;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void FixedUpdate()
    {
        if (gameManager.CurrentState == GameState.NotPlaying) return;
        // Движение камеры вперёд с фиксированной скоростью
        transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }
}
