using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPrefabSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool applicationIsQuitting = false;
    private static readonly string filepath = "MonoPrefabSingleton/";
    

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance of {typeof(T)} is null because application is quitting.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError($"[Singleton] Multiple instances of {typeof(T)} found!");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject prefab = Resources.Load<GameObject>(filepath + typeof(T));
                        KALLogger.Log(prefab!=null);
                        KALLogger.Log(filepath + typeof(T));
                        GameObject singleton = Instantiate(prefab);
                        singleton.name = $"(singleton) {typeof(T)}";
                        _instance = singleton.GetComponent<T>();
                        DontDestroyOnLoad(singleton);

                        Debug.Log($"[Singleton] An instance of {typeof(T)} was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log($"[Singleton] Using existing instance: {_instance.gameObject.name}");
                    }
                }

                return _instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
}

