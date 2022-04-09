

using UnityEngine;

public static class Extensions
{
    public static float FlatDistance(this Vector3 v, Vector3 other)
    {
        Vector3 c = v;
        c.y = 0;
        other.y = 0;

        return Vector3.Distance(c, other);
    }
}