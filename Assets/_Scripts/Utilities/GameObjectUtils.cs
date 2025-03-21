using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
    public static void EnableObjects(this List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    public static void DisableObjects(this List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
    public static List<GameObject> GetChildrenObjects(this Transform root)
    {
        List<GameObject> gameObjects = new();
        for (int i = 0, len = root.childCount; i < len; ++i)
        {
            gameObjects.Add(root.GetChild(i).gameObject);
        }
        return gameObjects;
    }
}
