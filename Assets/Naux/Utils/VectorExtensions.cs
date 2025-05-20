using System.Collections.Generic;
using UnityEngine;

namespace N.Utils
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Rotates a list of Vector2s clockwise by 90 degrees per step.
        /// </summary>
        public static List<Vector2> Rotate90CW(this List<Vector2> vectors, int steps)
        {
            List<Vector2> result = new(vectors.Count);
            foreach (var v in vectors)
                result.Add(v.Rotate90CW(steps));
            return result;
        }

        /// <summary>
        /// Rotates a list of Vector2s counter-clockwise by 90 degrees per step.
        /// </summary>
        public static List<Vector2> Rotate90CCW(this List<Vector2> vectors, int steps)
        {
            List<Vector2> result = new(vectors.Count);
            foreach (var v in vectors)
                result.Add(v.Rotate90CCW(steps));
            return result;
        }

        /// <summary>
        /// Rotates this Vector2 by 90 degrees counter-clockwise.
        /// </summary>
        public static Vector2 Rotate90CCW(this Vector2 v, int steps)
        {
            return v.Rotate90CW(-steps);
        }

        /// <summary>
        /// Rotates this Vector2 by 90 degrees clockwise (steps = 1 → 90°, 2 → 180°, etc).
        /// </summary>
        public static Vector2 Rotate90CW(this Vector2 v, int steps)
        {
            steps = ((steps % 4) + 4) % 4;

            return steps switch
            {
                0 => v,
                1 => new Vector2(v.y, -v.x),
                2 => new Vector2(-v.x, -v.y),
                3 => new Vector2(-v.y, v.x),
                _ => v
            };
        }

        /// <summary>
        /// Scales all Vector2s in the list by a float multiplier.
        /// </summary>
        public static List<Vector2> ScaleAll(this List<Vector2> vectors, float scale)
        {
            List<Vector2> result = new(vectors.Count);
            foreach (var v in vectors)
                result.Add(v * scale);
            return result;
        }

        // Replace X value of Vector3 with given value.
        public static Vector3 WithNewX(this Vector3 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        // Replace Y value of Vector3 with given value.
        public static Vector3 WithNewY(this Vector3 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        // Replace Z value of Vector3 with given value.
        public static Vector3 WithNewZ(this Vector3 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        // Replace X value of Vector2 with given value.
        public static Vector2 WithNewX(this Vector2 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        // Replace Y value of Vector2 with given value.
        public static Vector2 WithNewY(this Vector2 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector3 GetGlobalToLocalScaleFactor(Transform t)
        {
            Vector3 factor = Vector3.one;

            while (true)
            {
                Transform tParent = t.parent;

                if (tParent != null)
                {
                    factor.x *= tParent.localScale.x;
                    factor.y *= tParent.localScale.y;
                    factor.z *= tParent.localScale.z;

                    t = tParent;
                }
                else
                {
                    return factor;
                }
            }
        }
    }
}
