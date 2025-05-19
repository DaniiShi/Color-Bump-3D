using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, входит ли слой объекта в маску
        if (((1 << other.gameObject.layer) & playerLayer) != 0 && !isPlayerInTrigger)
        {
            isPlayerInTrigger = true;
            // Вызываем метод завершения игры
            GameManager.Instance.Finish();
        }
    }
}
