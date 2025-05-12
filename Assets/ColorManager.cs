using Unity.VisualScripting;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Header("Palettes")]
    public ColorPalette[] colorPalettes;

    [Header("Scene References")]
    public Camera mainCamera;

    public Material roadMaterial;
    public Material positiveMaterial;
    public Material negativeMaterial;

    public static ColorManager Instance { get; private set; }

    private ColorPalette currentPalette;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Применяем случайную палитру при старте
        ApplyRandomPalette();
    }

    /// <summary>
    /// Выбрать случайную палитру и применить
    /// </summary>
    public void ApplyRandomPalette()
    {
        if (colorPalettes.Length == 0)
        {
            Debug.LogWarning("ColorManager: No palettes assigned.");
            return;
        }

        int index = Random.Range(0, colorPalettes.Length);
        currentPalette = colorPalettes[index];
        ApplyPalette();
    }

    private void ApplyPalette()
    {
        if (mainCamera != null)
            mainCamera.backgroundColor = currentPalette.BackgroundColor;

        if (roadMaterial != null)
            roadMaterial.color = currentPalette.RoadColor;

        if (positiveMaterial != null)
            positiveMaterial.color = currentPalette.PositiveColor;

        if (negativeMaterial != null)
            negativeMaterial.color = currentPalette.NegativeColor;
    }

    public Color GetColor(ObjectType type)
    {
        return type switch
        {
            ObjectType.Positive => currentPalette.PositiveColor,
            ObjectType.Negative => currentPalette.NegativeColor,
            ObjectType.Road => currentPalette.RoadColor,
            _ => Color.black,
        };
    }
    public Material GetMaterial(ObjectType type)
    {
        return type switch
        {
            ObjectType.Positive => positiveMaterial,
            ObjectType.Negative => negativeMaterial,
            ObjectType.Road => roadMaterial,
            _ => null,
        };
    }

    public ColorPalette GetCurrentPalette()
    {
        return currentPalette;
    }
}


// Структура для хранения цветовой палитры
[System.Serializable]
public struct ColorPalette
{
    public Color PositiveColor;
    public Color NegativeColor;
    public Color BackgroundColor;
    public Color RoadColor;
}
