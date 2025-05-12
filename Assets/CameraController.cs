using UnityEngine;
using UnityEngine.Rendering.Universal;
using YG;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private float speed;

    public GameManager gameManager;


    private void Start()
    {
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetCameraRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        if (gameManager.CurrentState == GameState.NotPlaying) return;
        // Движение камеры вперёд с фиксированной скоростью
        transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }
}
