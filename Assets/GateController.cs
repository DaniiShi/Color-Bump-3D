using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public ObjectType objectTypeConvertTo;

    private void Start()
    {
        GetComponent<Renderer>().material.color = ColorManager.Instance.GetColor(objectTypeConvertTo);
    }
    private void OnTriggerEnter(Collider other)
    {
        ColorObject colorChanger = other.GetComponent<ColorObject>();
        if (colorChanger != null)
        {
            colorChanger.toggleType(objectTypeConvertTo, ColorManager.Instance.GetColor(objectTypeConvertTo));
        }
    }
}
