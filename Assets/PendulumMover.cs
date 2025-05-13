using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PendulumMover : MonoBehaviour
{
    public Vector3 axis = Vector3.right; // направление маятника (например, Vector3.right)
    public float amplitude = 1f;         // насколько далеко качается
    public float frequency = 1f;         // частота качания
    [Header("Активировать только на сложностях")]
    public List<LevelDifficulty> allowedDifficulties = new();
    private Rigidbody rb;
    private Vector3 startPosition;
    private bool unfreezed = false;
    private float time;
    private bool isActive;

    void Start()
    {
        isActive = allowedDifficulties.Contains(GameManager.Instance.currentLevelDifficulty);
        if (!isActive)
        {
            enabled = false; // просто отключаем скрипт, если сложность не входит
            return;
        }

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isActive || unfreezed) return;

        time += Time.deltaTime;
        Vector3 offset = axis.normalized * Mathf.Sin(time * frequency * Mathf.PI * 2f) * amplitude;
        transform.position = startPosition + offset;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isActive || unfreezed) return;

        unfreezed = true;
        rb.useGravity = true;

        // Подхватываем импульс из синусоиды
        rb.linearVelocity = axis.normalized * Mathf.Cos(time * frequency * Mathf.PI * 2f) * amplitude * frequency * 2f * Mathf.PI;
    }
}