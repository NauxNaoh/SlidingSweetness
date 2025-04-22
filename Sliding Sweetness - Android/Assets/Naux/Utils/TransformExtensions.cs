using UnityEngine;

namespace N.Utils
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Rotates the transform 90 degrees clockwise around the Z axis.
        /// </summary>
        /// <param name="transform">The transform to rotate.</param>
        public static void RotateClockwise2D(this Transform transform)
        {
            float z = transform.localEulerAngles.z;
            z = (z - 90f + 360f) % 360f;
            transform.localRotation = Quaternion.Euler(0f, 0f, z);
        }

        /// <summary>
        /// Rotates the transform 90 degrees counter-clockwise around the Z axis.
        /// </summary>
        /// <param name="transform">The transform to rotate.</param>
        public static void RotateCounterClockwise2D(this Transform transform)
        {
            float z = transform.localEulerAngles.z;
            z = (z + 90f) % 360f;
            transform.localRotation = Quaternion.Euler(0f, 0f, z);
        }


        /// <summary>
        /// Rotates the child to cancel out the parent's rotation on Z axis (for 2D look).
        /// Keeps the sprite visually upright even when the parent rotates.
        /// </summary>
        /// <param name="child">The child transform.</param>
        public static void CancelParentZRotation(this Transform child)
        {
            if (child.parent != null)
            {
                float parentZ = child.parent.localEulerAngles.z;
                child.localRotation = Quaternion.Euler(0f, 0f, -parentZ);
            }
        }

        /// <summary>
        /// Applies CancelParentZRotation() to all immediate children.
        /// </summary>
        /// <param name="parent">The parent transform.</param>
        public static void CancelChildrenZRotation(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                child.CancelParentZRotation();
            }
        }
    }
}
