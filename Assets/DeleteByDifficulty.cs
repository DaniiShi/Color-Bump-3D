using UnityEngine;
using System.Collections.Generic;

public class DeleteByDifficulty : MonoBehaviour
{
    [Header("Objects to DELETE for difficulty")]
    public List<GameObject> easyObjects = new();
    public List<GameObject> mediumObjects = new();
    public List<GameObject> hardObjects = new();

    private void Start()
    {
        ApplyDifficulty(GameManager.Instance.currentLevelDifficulty);
    }

    public void ApplyDifficulty(LevelDifficulty selected)
    {
        // Соберём те, что нужно удалить
        HashSet<GameObject> toDelete = selected switch
        {
            LevelDifficulty.Easy => new HashSet<GameObject>(easyObjects),
            LevelDifficulty.Medium => new HashSet<GameObject>(mediumObjects),
            LevelDifficulty.Hard => new HashSet<GameObject>(hardObjects),
            _ => new HashSet<GameObject>()
        };

        foreach (Transform child in transform)
        {
            if (toDelete.Contains(child.gameObject))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
