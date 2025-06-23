using UnityEngine;

namespace Undercooked.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                var objects = FindObjectsOfType<T>();
                if (objects.Length > 0)
                    _instance = objects[0];
                if (objects.Length > 1)
                    Debug.LogError($"[Singleton] There is more than one {typeof(T).Name} in the scene.");

                if (_instance == null)
                {
                    Debug.LogWarning($"[Singleton] No instance of {typeof(T).Name} found. Creating one.");
                    GameObject obj = new GameObject($"_{typeof(T).Name}");
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject); // optional: destroy duplicate
            }
        }
    }
}
