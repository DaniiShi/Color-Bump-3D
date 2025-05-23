using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MirrorByX : EditorWindow
{
    [MenuItem("Tools/Mirror Selected (X Axis)")]
    public static void MirrorSelected()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("Выдели хотя бы один объект для отражения.");
            return;
        }

        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        Scene targetScene = prefabStage != null ? prefabStage.scene : SceneManager.GetActiveScene();

        foreach (GameObject obj in selectedObjects)
        {
            // Получаем оригинальный объект
            GameObject copy;

            if (PrefabUtility.IsPartOfPrefabInstance(obj))
            {
                var sourcePrefab = PrefabUtility.GetCorrespondingObjectFromSource(obj);
                copy = (GameObject)PrefabUtility.InstantiatePrefab(sourcePrefab, targetScene);
            }
            else
            {
                copy = Object.Instantiate(obj);
                SceneManager.MoveGameObjectToScene(copy, targetScene);
            }

            // Отражаем по X
            Vector3 pos = obj.transform.position;
            pos.x = -pos.x;
            copy.transform.position = pos;

            // Инвертируем Y-вращение (зависит от нужного поведения)
            Vector3 rot = obj.transform.eulerAngles;
            rot.y = -rot.y;
            copy.transform.eulerAngles = rot;

            // Оставляем масштаб как есть (можно опционально инвертировать X при необходимости)
            copy.name = obj.name + "_Mirrored";
            copy.transform.SetParent(obj.transform.parent);
        }

        Debug.Log($"✅ Отражено {selectedObjects.Length} объектов по оси X.");
    }
}