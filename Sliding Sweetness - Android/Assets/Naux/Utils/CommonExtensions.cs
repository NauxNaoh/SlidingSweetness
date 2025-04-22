using UnityEngine;

namespace N.Utils
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Get mouse position on screen
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static void SetActiveObject(this GameObject obj, bool status)
        {
            if (obj.activeSelf == status) return;
            obj.SetActive(status);
        }

    }
}
