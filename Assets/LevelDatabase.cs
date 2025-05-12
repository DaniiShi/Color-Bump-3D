using System.Collections.Generic;
using UnityEngine;

public class LevelDatabase : MonoBehaviour
{
    public static LevelDatabase Instance { get; private set; }

    private LevelSet levelSet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadLevels()
    {
        TextAsset json = Resources.Load<TextAsset>("generated_levels");
        if (json != null)
        {
            levelSet = JsonUtility.FromJson<LevelSet>(json.text);
            Debug.Log($"Loaded {levelSet.levels.Count} levels.");
        }
        else
        {
            Debug.LogError("Could not load levels JSON from Resources/generated_levels.json");
        }
    }

    public LevelData GetLevel(int index)
    {
        if (index >= levelSet.levels.Count)
            index = levelSet.levels.Count - 1;

        return levelSet.levels[index];
    }
}
[System.Serializable]
public class LevelData
{
    public List<int> chunks;
    public int? speed;
    public int? cameraRotation;
}

[System.Serializable]
public class LevelSet
{
    public List<LevelData> levels;
}
