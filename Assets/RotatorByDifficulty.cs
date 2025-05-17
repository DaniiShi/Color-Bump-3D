using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotatorByDifficulty : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float rotationSpeed = 90f; // градусов в секунду

    [Header("Активировать только на сложностях")]
    public List<LevelDifficulty> allowedDifficulties = new();

    private bool isActive;
    private bool unfreezed = false;
    private Rigidbody rb;

    void Start()
    {
        isActive = allowedDifficulties.Contains(GameManager.Instance.currentLevelDifficulty);
        if (!isActive)
        {
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (!isActive || unfreezed) return;

        transform.Rotate(axis.normalized * rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isActive || unfreezed) return;

        unfreezed = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        // Можно задать начальную угловую скорость
        rb.angularVelocity = axis.normalized * rotationSpeed * Mathf.Deg2Rad;
    }
}
