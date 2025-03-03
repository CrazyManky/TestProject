using UnityEngine;

namespace _Project.Scripts.MathUtils
{
    public struct MathfDistance
    {
        public static float Calculate(Vector3 one, Vector3 two)
        {
            return Mathf.Sqrt((one - two).sqrMagnitude);
        }
    }
}