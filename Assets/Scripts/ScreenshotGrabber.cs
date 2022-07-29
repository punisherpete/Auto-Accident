#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ScreenshotGrabber
{
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {
        ScreenCapture.CaptureScreenshot("Screenshot" + GetCh() + GetCh() + GetCh() + "_" + Screen.width + "x" + Screen.height + ".png", 1);
    }

    public static char GetCh()
    {
        return (char)UnityEngine.Random.Range('A', 'Z');
    }
}
#endif