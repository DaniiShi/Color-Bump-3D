using System.Collections.Generic;
using UnityEngine;

public class SwipeObjectType : MonoBehaviour
{
    [Header("Сложности, при которых активировать поведение")]
    public List<LevelDifficulty> allowedDifficulties = new();

    [Header("Тип объекта при активации")]
    public ObjectType objectType;

    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        var difficulty = gameManager.currentLevelDifficulty;
        if (!allowedDifficulties.Contains(difficulty)) return;

        var colorObj = GetComponent<ColorObject>();
        if (colorObj != null)
        {
            colorObj.objectType = objectType;

            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = gameManager.currentColorPalette.GetColorByType(objectType);
            }
        }
    }
}
