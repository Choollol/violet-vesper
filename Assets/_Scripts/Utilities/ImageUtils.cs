using UnityEngine;
using UnityEngine.UI;

public static class ImageUtils
{
    public static void SetAlpha(this Image image, float opacity)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }
}
