using UnityEngine;

namespace _Project.Scripts.MathUtils
{
    public struct MathfDistance
    {
        public static float Calculate(Transform one, Transform two)
        {
            return Mathf.Sqrt((one.position - two.position).sqrMagnitude);
        }
    }
}