using UnityEngine;

namespace DefaultNamespace {
    public static class Vector3Extensions {
        public static Vector3 Random(this Vector3 vector3, float magnitude = 0) {
            if (magnitude != 0)
                return new Vector3(RandValue(), RandValue(), RandValue()) * magnitude;
            else
                return new Vector3(RandValue(), RandValue(), RandValue());

            float RandValue() => UnityEngine.Random.value * (UnityEngine.Random.value > 0.5f ? 1f : -1f);
        }
    }
}