using System.Collections.Generic;
using UnityEngine;

public static class CameraUtils
{
    public static IEnumerator<Camera> FadeColor(Camera camera, Color targetColor, float duration)
    {
        if (camera.backgroundColor == targetColor) { yield break; }
        float currentTime = 0;
        Color start = camera.backgroundColor;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            camera.backgroundColor = Color.Lerp(start, targetColor, currentTime / duration);
            yield return null;
        }
        camera.backgroundColor = targetColor;
        yield break;
    }
}
