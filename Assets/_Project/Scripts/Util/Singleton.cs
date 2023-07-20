using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Similar to singleton, but overrides current instance rather than destroying new attempted instances.
    /// </summary>
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Singleton class. Only allows one static instance of a class.
    /// </summary>
    public abstract class Singletone<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            base.Awake();
        }
    }

    /// <summary>
    /// Persistant version of singleton. Survives through scene loads.
    /// </summary>
    public abstract class PersistantSingleton<T> : Singletone<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}

