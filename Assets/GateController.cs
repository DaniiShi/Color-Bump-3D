using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public ObjectType objectTypeConvertTo;
    private Color convertToColor;
    Material material;

    private void Awake()
    {
        convertToColor = GameManager.Instance.currentColorPalette.GetColorByType(objectTypeConvertTo);
        Renderer spriteRenderer = GetComponent<Renderer>();
        material = spriteRenderer.material;
        material.SetColor("_HologramTint", convertToColor);
        foreach (var item in GetComponentsInChildren<Renderer>())
        {
            item.material.color = new Color(convertToColor.r, convertToColor.g, convertToColor.b, 1);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        ColorObject colorChanger = other.GetComponent<ColorObject>();
        if (colorChanger != null)
        {
            colorChanger.toggleType(objectTypeConvertTo, convertToColor);
        }
    }
}
