using System.Collections.Generic;
using UnityEngine;

public static class SpriteRendererUtils
{
    public static void SetAlpha(this SpriteRenderer spriteRenderer, float opacity)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
    }
    public static void AddAlpha(this SpriteRenderer spriteRenderer, float amount)
    {
        spriteRenderer.color += new Color(0, 0, 0, amount);
    }
    public static IEnumerator<SpriteRenderer> FadeAlpha(SpriteRenderer spriteRenderer, float targetOpacity, float duration)
    {
        if (spriteRenderer.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = spriteRenderer.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            spriteRenderer.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        spriteRenderer.SetAlpha(targetOpacity);
        yield break;
    }
}
