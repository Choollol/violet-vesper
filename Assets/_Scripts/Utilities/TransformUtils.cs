using System.Collections.Generic;
using UnityEngine;

public static class TransformUtils
{
    public static void SetPosition(this Transform transform, Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    public static void SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public static void SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    public static void SetLocalPositionX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }
    public static void SetLocalPositionY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }
    public static void AddRotation(this Transform transform, float angle)
    {
        transform.rotation *= Quaternion.Euler(0, 0, angle);
    }
    public static void SetLocalScaleX(this Transform transform, float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
    public static void SetLocalScaleY(this Transform transform, float scaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
    public static void SetLocalScaleZ(this Transform transform, float scaleZ)
    {
        transform.localScale = new Vector3(transform.localScale.z, transform.localScale.y, scaleZ);
    }
    public static void SetRotationX(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
    public static void SetRotationY(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
    }
    public static void SetRotationZ(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
    }
    public static void SetLocalRotationX(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(angle, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
    public static void SetLocalRotationY(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, angle, transform.localRotation.eulerAngles.z);
    }
    public static void SetLocalRotationZ(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, angle);
    }

    /// <summary>
    /// Returns a list containing the transforms of children objects without their own children
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static List<Transform> GetChildTransforms(this Transform root)
    {
        List<Transform> list = new List<Transform>();
        StoreTransforms(root, list);
        return list;
    }
    private static void StoreTransforms(Transform root, List<Transform> list)
    {
        if (root.childCount == 0)
        {
            list.Add(root);
        }
        else
        {
            for (int i = 0, len = root.childCount; i < len; i++)
            {
                StoreTransforms(root.GetChild(i), list);
            }
        }
    }
    public static void EnableTransforms(this List<Transform> transforms)
    {
        foreach (Transform transform in transforms)
        {
            transform.gameObject.SetActive(true);
        }
    }
    public static void DisableTransforms(this List<Transform> transforms)
    {
        foreach (Transform transform in transforms)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
