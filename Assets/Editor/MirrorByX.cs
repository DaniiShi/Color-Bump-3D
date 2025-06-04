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

        Undo.IncrementCurrentGroup();
        int undoGroup = Undo.GetCurrentGroup();

        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        Scene targetScene = prefabStage != null ? prefabStage.scene : SceneManager.GetActiveScene();

        foreach (GameObject obj in selectedObjects)
        {
            GameObject copy;

            if (PrefabUtility.IsPartOfPrefabInstance(obj))
            {
                GameObject sourcePrefab = PrefabUtility.GetCorrespondingObjectFromSource(obj);
                copy = (GameObject)PrefabUtility.InstantiatePrefab(sourcePrefab, targetScene);
            }
            else
            {
                copy = Object.Instantiate(obj);
                SceneManager.MoveGameObjectToScene(copy, targetScene);
            }

            Undo.RegisterCreatedObjectUndo(copy, "Mirror Object");

            // Отражаем позицию по X
            Vector3 pos = obj.transform.position;
            pos.x = -pos.x;
            copy.transform.position = pos;

            // Инвертируем поворот по Y (если нужно)
            Vector3 rot = obj.transform.eulerAngles;
            rot.y = -rot.y;
            copy.transform.eulerAngles = rot;

            // Сохраняем остальные параметры
            copy.transform.localScale = obj.transform.localScale;
            copy.name = obj.name + "_Mirrored";
            copy.transform.SetParent(obj.transform.parent, true);
        }

        Undo.CollapseUndoOperations(undoGroup);
        Debug.Log($"✅ Отражено {selectedObjects.Length} объектов по оси X с поддержкой Ctrl+Z.");
    }
}