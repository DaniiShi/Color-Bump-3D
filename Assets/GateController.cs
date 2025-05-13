using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public ObjectType objectTypeConvertTo;
    private Color convertToColor;

    private void Awake()
    {
        print(GetComponent<Renderer>().material.color);
        convertToColor = GameManager.Instance.currentColorPalette.GetColorByType(objectTypeConvertTo);
        GetComponent<Renderer>().material.color = convertToColor;
        print(GetComponent<Renderer>().material.color);

    }
    private void OnTriggerEnter(Collider other)
    {
        ColorObject colorChanger = other.GetComponent<ColorObject>();
        if (colorChanger != null)
        {
            colorChanger.toggleType(objectTypeConvertTo, GameManager.Instance.currentColorPalette.GetColorByType(objectTypeConvertTo));
        }
    }
}
