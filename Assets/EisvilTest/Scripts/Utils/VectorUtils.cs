using UnityEngine;

public static class VectorUtils
{
    public static Vector3 XYtoXZ(this Vector2 from)
    {
        return new Vector3(from.x, 0, from.y);
    }
}