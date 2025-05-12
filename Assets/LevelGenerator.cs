using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ChunkEntry
{
    public GameObject prefab;
    public float length = 10f;
}

[System.Serializable]
public class LevelPartData
{
    public GameObject prefab; // Префаб части уровня
    public float length; // Длина части уровня по Z
    public int baseWeight = 1; // Базовый вес
    public AnimationCurve weightCurve; // Кривая изменения веса от уровня
}
public class LevelGenerator : MonoBehaviour
{
    const int baseLevelSpeed = 4;
    public CameraController cameraController;
    public PlayerController playerController;
    [Header("Chunk Prefabs")]
    public ChunkEntry[] chunkEntries; // Индекс соответствует JSON

    [Header("Level Settings")]
    public Vector3 startSpawnPosition = new Vector3(0, 0, 15);

    private List<GameObject> activeChunks = new();
    private Vector3 currentSpawnPosition;
    public MainObjMovement mainObjMovement;

    private void Start()
    {
        currentSpawnPosition = startSpawnPosition;

        // Применяем случайную палитру
        ColorManager.Instance.ApplyRandomPalette();
        GameManager gameManager = GameManager.Instance;

        GenerateLevel(gameManager.currentLevelIndex);
    }

    public void GenerateLevel(int levelIndex)
    {
        currentSpawnPosition = startSpawnPosition;

        ClearLevel();

        LevelData levelData = LevelDatabase.Instance.GetLevel(levelIndex);

        mainObjMovement.SetSpeed(levelData.speed ?? baseLevelSpeed);
        cameraController.SetCameraRotation(levelData.cameraZRotation ?? 0);
        GameManager.Instance.currentLevelDifficulty = levelData.difficulty ?? LevelDifficulty.Easy;

        foreach (int chunkIndex in levelData.chunks)
        {
            SpawnChunk(chunkIndex);
        }
    }

    private void SpawnChunk(int chunkIndex)
    {
        if (chunkIndex < 0 || chunkIndex >= chunkEntries.Length)
        {
            Debug.LogWarning($"Invalid chunk index {chunkIndex}");
            return;
        }

        var entry = chunkEntries[chunkIndex];

        // Сдвигаем вперёд на ПОЛОВИНУ длины ПЕРЕД спавном (центрирован)
        currentSpawnPosition.z += entry.length / 2f;

        GameObject chunk = Instantiate(entry.prefab, currentSpawnPosition, Quaternion.identity);
        activeChunks.Add(chunk);

        // После спавна — подвигаемся ещё на половину длины (центр следующего чанка)
        currentSpawnPosition.z += entry.length / 2f;
    }

    private void ClearLevel()
    {
        foreach (var chunk in activeChunks)
        {
            Destroy(chunk);
        }

        activeChunks.Clear();
    }
}
