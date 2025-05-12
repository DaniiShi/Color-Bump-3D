using UnityEngine;

public class RandomReverceColor : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ColorManager colorManager = ColorManager.Instance;
        // ColorObject[] colorObjects = GetComponentsInChildren<ColorObject>();
        // // Генерируем случайное число (0 или 1)
        // int inverseFactor = Random.Range(0, 2); // Random.Range(0, 2) даёт 0 или 1

        // // Если inverseFactor равен 1, инвертируем цвета
        // if (inverseFactor == 1)
        // {
        //     foreach (ColorObject colorObject in colorObjects)
        //     {
        //         if (colorObject.objectType != ObjectType.Road)
        //         {
        //             // Инвертируем тип объекта
        //             colorObject.objectType = (colorObject.objectType == ObjectType.Positive)
        //                 ? ObjectType.Negative
        //                 : ObjectType.Positive;

        //             // Устанавливаем новый цвет
        //             colorObject.SetMaterial(colorManager.GetMaterial(colorObject.objectType));
        //         }

        //     }
        // }

    }
}
