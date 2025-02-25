using System;
using UnityEngine;

public static class StringUtils
{
    public static string RemoveSpaces(this string text)
    {
        return text.Replace(" ", "");
    }
    public static string AddSpaces(this string text)
    {
        string finalText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && i != 0)
            {
                finalText += " ";
            }
            finalText += text[i];
        }
        return finalText;
    }
    public static string ToCamelCase(this string text)
    {
        string finalText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 0)
            {
                finalText += char.ToLower(text[0]);
            }
            else if (text[i] != ' ')
            {
                finalText += text[i];
            }
        }
        return finalText;
    }
    public static KeyCode ToKeyCode(string s)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), s, true);
    }
}
