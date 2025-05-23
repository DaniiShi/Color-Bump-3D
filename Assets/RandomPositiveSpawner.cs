using System.Collections.Generic;
using UnityEngine;

public class RandomPositiveSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject positivePrefab;
    public GameObject negativePrefab;

    [Header("Настройки")]
    public int count = 5;               // Всего объектов
    public int positiveCount = 1;       // Сколько подряд Positive
    public float spacing = 1.5f;        // Расстояние между объектами по X

    void Start()
    {
        if (count < 1 || positiveCount < 1 || positiveCount > count)
        {
            Debug.LogWarning("❌ Проверь параметры: count >= 1, 1 <= positiveCount <= count");
            return;
        }

        // Выбираем случайный старт для блока positive
        int startIndex = Random.Range(0, count - positiveCount + 1);

        for (int i = 0; i < count; i++)
        {
            bool isPositive = i >= startIndex && i < startIndex + positiveCount;

            GameObject prefabToSpawn = isPositive ? positivePrefab : negativePrefab;

            Vector3 spawnPos = transform.position + new Vector3((i - count / 2f) * spacing, 0f, 0f);

            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, transform);
        }
    }
}
