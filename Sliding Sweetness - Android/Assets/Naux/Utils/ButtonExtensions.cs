using UnityEngine.Events;
using UnityEngine.UI;

namespace N.Utils
{
    public static class ButtonExtensions
    {
        /// <summary>
        /// Register action event of button
        /// </summary>
        public static void RegisterEventButton(this Button button, UnityAction unityAction, bool clearListeners = true)
        {
            if (clearListeners)
                button.onClick.RemoveAllListeners();
            if (unityAction != null)
                button.onClick.AddListener(unityAction);
        }
    }
}