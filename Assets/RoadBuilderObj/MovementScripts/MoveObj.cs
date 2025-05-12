using UnityEngine;

public class MoveObj : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f; // Скорость движения
    public float distance = 5f; // Расстояние, на которое объект движется в одну сторону
    public Vector3 direction = Vector3.right; // Направление движения (по умолчанию вправо)

    private Vector3 startPosition; // Начальная позиция объекта
    private bool isStopped = false; // Остановлен ли объект

    private Rigidbody rb;

    private void Start()
    {
        // Сохраняем начальную позицию объекта
        startPosition = transform.position;

        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();

        // Если Rigidbody отсутствует, добавляем его
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Делаем Rigidbody кинематическим, чтобы он не реагировал на физику
        }

        // Нормализуем направление, чтобы избежать проблем с разной скоростью
        direction = direction.normalized;
    }

    private void Update()
    {
        // Если объект остановлен, прекращаем движение
        if (isStopped) return;

        // Двигаем объект в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime);

        // Если объект достиг максимальной дистанции, меняем направление на противоположное
        if (Vector3.Distance(transform.position, startPosition) >= distance)
        {
            direction = -direction; // Инвертируем направление
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Если объект сталкивается с чем-либо, останавливаем его
        isStopped = true;
        Debug.Log("Object stopped due to collision with: " + collision.gameObject.name);
    }
}
