using UnityEngine;

public class LoadPrefByDifficulty : MonoBehaviour
{
    [Header("Сложности, при которых загружается объект")]
    public LevelDifficulty[] allowedDifficulties;

    [Header("Prefab, который нужно загрузить")]
    public GameObject prefabToLoad;

    [Header("Позиция и поворот")]
    public Vector3 localOffset = Vector3.zero;
    public Vector3 rotationEuler = Vector3.zero;

    [Header("Destroy loader после загрузки")]
    public bool destroyAfterLoad = true;

    void Start()
    {
        LevelDifficulty currentDifficulty = GameManager.Instance.currentLevelDifficulty;

        if (!IsAllowed(currentDifficulty))
        {
            if (destroyAfterLoad) Destroy(gameObject);
            return;
        }

        if (prefabToLoad != null)
        {
            Quaternion rotation = Quaternion.Euler(rotationEuler);
            Vector3 position = transform.position + localOffset;

            GameObject loaded = Instantiate(prefabToLoad, position, rotation, transform);
        }

        if (destroyAfterLoad)
        {
            Destroy(this); // удаляем только скрипт
        }
    }

    private bool IsAllowed(LevelDifficulty difficulty)
    {
        foreach (var d in allowedDifficulties)
        {
            if (d == difficulty)
                return true;
        }
        return false;
    }
}
