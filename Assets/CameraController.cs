using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;


    public void SetCameraRotation(float ZRotation)
    {
        // Устанавливаем вращение камеры по оси Z
        Quaternion rotation = Quaternion.Euler(55, 0, ZRotation);
        transform.rotation = rotation;
    }

}
