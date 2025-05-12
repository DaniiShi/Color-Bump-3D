using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PendulumMover : MonoBehaviour
{
    public Vector3 axis = Vector3.right; // направление маятника (например, Vector3.right)
    public float amplitude = 1f;         // насколько далеко качается
    public float frequency = 1f;         // частота качания

    private Rigidbody rb;
    private Vector3 startPosition;
    private bool unfreezed = false;
    private float time;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition;
        startPosition = transform.position;
    }

    void Update()
    {
        if (unfreezed) return;

        time += Time.deltaTime;

        // Позиция = старт + смещение по синусоиде вдоль оси
        Vector3 offset = axis.normalized * Mathf.Sin(time * frequency * Mathf.PI * 2f) * amplitude;
        transform.position = startPosition + offset;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (unfreezed) return;

        unfreezed = true;

        // Снимаем фриз — теперь Rigidbody подчиняется физике
        rb.constraints = RigidbodyConstraints.None;
        rb.linearVelocity = axis.normalized * Mathf.Sin(time * frequency * Mathf.PI * 2f) * amplitude * frequency * 2f * Mathf.PI;
    }
}