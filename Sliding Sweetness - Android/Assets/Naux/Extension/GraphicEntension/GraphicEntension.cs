using UnityEngine;
using UnityEngine.UI;

namespace Naux.UIExtension
{
    public static class GraphicEntension
    {
        public static void SetAlpha(this SpriteRenderer spr, float alpha)
        {
            var _newColor = spr.color;
            _newColor.a = alpha;
            spr.color = _newColor;
        }

        public static void SetAlpha(this Image img, float alpha)
        {
            var _newColor = img.color;
            _newColor.a = alpha;
            img.color = _newColor;
        }
    }

    public static class TransformExtension
    {
        public static void SetActiveObject(this GameObject obj, bool status)
        {
            if (obj.activeSelf == status) return;
            obj.SetActive(status);
        }

        public static Vector3 GetMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}