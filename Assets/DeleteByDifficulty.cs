using UnityEngine;
using System.Collections.Generic;

public class DeleteByDifficulty : MonoBehaviour
{
    [Header("Objects to keep for difficulty")]
    public List<GameObject> easyObjects = new();
    public List<GameObject> mediumObjects = new();
    public List<GameObject> hardObjects = new();

    private void Start()
    {
        ApplyDifficulty(GameManager.Instance.currentLevelDifficulty);
    }

    public void ApplyDifficulty(LevelDifficulty selected)
    {
        // Соберём все, что нужно удалить
        HashSet<GameObject> toKeep = selected switch
        {
            LevelDifficulty.Easy => new HashSet<GameObject>(easyObjects),
            LevelDifficulty.Medium => new HashSet<GameObject>(mediumObjects),
            LevelDifficulty.Hard => new HashSet<GameObject>(hardObjects),
            _ => new HashSet<GameObject>()
        };

        foreach (Transform child in transform)
        {
            if (!toKeep.Contains(child.gameObject))
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(child.gameObject);
                else
                    Destroy(child.gameObject);
#else
                Destroy(child.gameObject);
#endif
            }
        }
    }
}
