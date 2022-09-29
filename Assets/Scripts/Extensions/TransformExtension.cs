using UnityEngine;

public static class TransformExtension
{
    public static Vector3 EulerAsInspector(this Transform t)
    {
        var euler = t.localEulerAngles;
        var x = WrapAngle(euler.x);
        var y = WrapAngle(euler.y);
        var z = WrapAngle(euler.z);

        return new Vector3(x, y, z);
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
        {
            return angle - 360;
        }

        return angle;
    }
}