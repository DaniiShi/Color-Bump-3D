using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style;
    Rect rect;
    float fps;
    string fpsText;

    void Start()
    {
        int w = Screen.width, h = Screen.height;

        rect = new Rect(10, 10, w, h * 2 / 100);

        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = Color.white;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        fpsText = string.Format("{0:0.} FPS", fps);
    }

    void OnGUI()
    {
        GUI.Label(rect, fpsText, style);
    }
}
