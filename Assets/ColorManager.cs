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

    public GameManager gameManager;


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
        ColorPalette selectedPalette = colorPalettes[index];
        gameManager.currentColorPalette = selectedPalette;
        ApplyPalette(selectedPalette);
    }

    private void ApplyPalette(ColorPalette palette)

    {
        if (mainCamera != null)
            mainCamera.backgroundColor = palette.BackgroundColor;

        if (roadMaterial != null)
            roadMaterial.color = palette.RoadColor;

        if (positiveMaterial != null)
            positiveMaterial.color = palette.PositiveColor;

        if (negativeMaterial != null)
            negativeMaterial.color = palette.NegativeColor;
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
}


// Структура для хранения цветовой палитры
[System.Serializable]
public struct ColorPalette
{
    public Color PositiveColor;
    public Color NegativeColor;
    public Color BackgroundColor;
    public Color RoadColor;


    public Color GetColorByType(ObjectType type)
    {
        return type switch
        {
            ObjectType.Positive => this.PositiveColor,
            ObjectType.Negative => NegativeColor,
            ObjectType.Road => RoadColor,
            _ => Color.black,
        };
    }
}
