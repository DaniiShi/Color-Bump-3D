using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MirrorByZ
{
    [MenuItem("Tools/Mirror Selected (Z Axis)")]
    public static void MirrorSelectedZ()
    {
        Mirror(Axis.Z);
    }

    private enum Axis { Z }

    private static void Mirror(Axis axis)
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("Выдели хотя бы один объект.");
            return;
        }

        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        Scene targetScene = prefabStage != null ? prefabStage.scene : SceneManager.GetActiveScene();

        foreach (GameObject obj in selectedObjects)
        {
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

            Vector3 pos = obj.transform.position;
            pos.z = -pos.z;
            copy.transform.position = pos;

            Vector3 rot = obj.transform.eulerAngles;
            rot.y = -rot.y;
            copy.transform.eulerAngles = rot;

            copy.name = obj.name + "_MirrorZ";
            copy.transform.SetParent(obj.transform.parent);
        }

        Debug.Log($"✅ Отражено {selectedObjects.Length} объектов по Z.");
    }
}