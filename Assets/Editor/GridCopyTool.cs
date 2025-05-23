using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridCopier : EditorWindow
{
    private int columns = 5;
    private int rows = 5;
    private int layersY = 1; // <-- Новое поле
    private float spacingX = 1.5f;
    private float spacingZ = 1.5f;
    private float spacingY = 1.5f; // <-- Новое поле

    [MenuItem("Tools/Grid Copier")]
    public static void ShowWindow()
    {
        GetWindow<GridCopier>("Grid Copier");
    }

    private void OnGUI()
    {
        GUILayout.Label("🧱 3D Сетка копий", EditorStyles.boldLabel);

        columns = EditorGUILayout.IntField("Кол-во по X", Mathf.Max(1, columns));
        rows = EditorGUILayout.IntField("Кол-во по Z", Mathf.Max(1, rows));
        layersY = EditorGUILayout.IntField("Кол-во по Y", Mathf.Max(1, layersY));
        spacingX = EditorGUILayout.FloatField("Отступ по X", spacingX);
        spacingZ = EditorGUILayout.FloatField("Отступ по Z", spacingZ);
        spacingY = EditorGUILayout.FloatField("Отступ по Y", spacingY);

        if (GUILayout.Button("Создать сетку"))
        {
            CreateGrid();
        }
    }

    private void CreateGrid()
    {
        GameObject selected = Selection.activeGameObject;
        if (selected == null)
        {
            Debug.LogWarning("Выдели объект в сцене или префабе.");
            return;
        }

        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        Scene targetScene = prefabStage != null ? prefabStage.scene : SceneManager.GetActiveScene();

        Vector3 basePosition = selected.transform.position;
        Transform parentTransform = selected.transform.parent;

        GameObject lastCopy = null;

        for (int y = 0; y < layersY; y++)
        {
            for (int z = 0; z < rows; z++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (x == 0 && z == 0 && y == 0) continue; // Пропускаем оригинал

                    Vector3 offset = new Vector3(x * spacingX, y * spacingY, z * spacingZ);
                    GameObject copy;

                    if (PrefabUtility.IsPartOfPrefabInstance(selected))
                    {
                        var sourcePrefab = PrefabUtility.GetCorrespondingObjectFromSource(selected);
                        copy = (GameObject)PrefabUtility.InstantiatePrefab(sourcePrefab, targetScene);
                    }
                    else
                    {
                        copy = Instantiate(selected);
                        SceneManager.MoveGameObjectToScene(copy, targetScene);
                    }

                    copy.name = selected.name + $"_({x},{y},{z})";
                    copy.transform.position = basePosition + offset;
                    copy.transform.rotation = selected.transform.rotation;
                    copy.transform.localScale = selected.transform.localScale;
                    copy.transform.SetParent(parentTransform, true);

                    lastCopy = copy;
                }
            }
        }

        if (lastCopy != null)
        {
            SceneView.lastActiveSceneView.FrameSelected();
        }

        Debug.Log($"✅ Сетка {columns}x{layersY}x{rows} создана.");
    }
}