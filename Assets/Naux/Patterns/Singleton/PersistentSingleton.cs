using UnityEngine;

namespace N.Patterns
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool AutoUnparentOnAwake = true;
        private static T instance;

        internal static bool HasInstance => instance != null;
        internal static T TryGetInstance() => HasInstance ? instance : null;
        internal static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        instance = go.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        void InitializeSingleton()
        {
            if (!Application.isPlaying) return;

            if (AutoUnparentOnAwake)
                transform.SetParent(null);

            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                    Destroy(gameObject);
            }
        }
    }
}