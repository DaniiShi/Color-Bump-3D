using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ChunkEntry
{
    public GameObject prefab;
    public float length = 10f;
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
    public ColorManager colorManager;
    public GameManager gameManager;

    [Header("Finish")]
    public GameObject finishPrefab; // Префаб финиша
    private float finishOffsetZ = 25f; // Можно задать дополнительный отступ, если нужно

    private void Start()
    {
        currentSpawnPosition = startSpawnPosition;

        // Применяем случайную палитру
        colorManager.ApplyRandomPalette();

        GenerateLevel(gameManager.currentLevelIndex);
    }

    private void GenerateLevel(int levelIndex)
    {
        currentSpawnPosition = startSpawnPosition;

        ClearLevel();

        LevelData levelData = LevelDatabase.Instance.GetLevel(levelIndex);

        Debug.Log($"{levelData.difficulty}");

        mainObjMovement.SetSpeed(levelData.speed ?? baseLevelSpeed);
        cameraController.SetCameraRotation(levelData.cameraZRotation ?? 0);

        GameManager.Instance.currentLevelDifficulty = LevelDifficultyHelper.GetByString(levelData.difficulty);

        foreach (int chunkIndex in levelData.chunks)
        {
            SpawnChunk(chunkIndex);
        }

        // Вставка финиша в конец уровня
        if (finishPrefab != null)
        {
            Vector3 finishPosition = currentSpawnPosition;
            finishPosition.z += finishOffsetZ;

            GameObject finish = Instantiate(finishPrefab, finishPosition, Quaternion.identity);
            activeChunks.Add(finish);
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
