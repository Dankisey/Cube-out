using UnityEngine;

public static class Utils
{
    public static bool Approximately(Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        float aSum = a.x + a.y + a.z;
        float bSum = b.x + b.y + b.z;

        return aSum - tolerance < bSum && aSum + tolerance > bSum;
    }
}