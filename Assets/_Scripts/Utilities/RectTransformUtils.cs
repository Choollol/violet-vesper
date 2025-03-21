using UnityEngine;

public static class RectTransformUtils
{    public static void SetPosX(this RectTransform rectTransform, float x)
    {
        rectTransform.position = new Vector3(x, rectTransform.position.y, rectTransform.position.z);
    }
    public static void SetPosY(this RectTransform rectTransform, float y)
    {
        rectTransform.position = new Vector3(rectTransform.position.x, y, rectTransform.position.z);
    }
    public static void SetWidth(this RectTransform rectTransform, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }
    public static void SetHeight(this RectTransform rectTransform, float height)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }
}
