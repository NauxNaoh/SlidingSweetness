using UnityEngine;
using UnityEngine.UI;

namespace N.Utils
{
    public static class GraphicExtensions
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
}
