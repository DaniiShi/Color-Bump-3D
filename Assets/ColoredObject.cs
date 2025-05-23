using UnityEngine;


[System.Serializable]
public enum ObjectType
{
    Positive,
    Negative,
    Road,
    Neutral
}

public class ColorObject : MonoBehaviour
{
    public ObjectType objectType;
    public bool isPlayer;

    public void toggleType(ObjectType type, Color colorTo)
    {
        objectType = isPlayer ? type : ObjectType.Neutral;
        GetComponent<Renderer>().material.color = colorTo;
    }
    public void setColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void SetMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
    }

}

