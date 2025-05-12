using UnityEngine;

public class DieCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, есть ли у объекта компонент ColorObject
        ColorObject otherColorObject = collision.gameObject.GetComponent<ColorObject>();
        if (otherColorObject != null)
        {
            ObjectType currentObjectType = GetComponentInChildren<ColorObject>().objectType;
            // Если цвета не совпадают, сфера разрушается
            if (otherColorObject.objectType != ObjectType.Neutral && otherColorObject.objectType != ObjectType.Road && otherColorObject.objectType != currentObjectType)
            {
                Debug.Log("Die");
                // DestroySphere();
            }
        }
    }
}
