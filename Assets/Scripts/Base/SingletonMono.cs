using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _isApplicationQuitting;

    [SerializeField] protected bool global;

    public static T Instance
    {
        get
        {
            if (_isApplicationQuitting)
            {
                Debug.LogWarning($"单例 {typeof(T)} 已被销毁");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    // 场景中找不到则新建对象
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name + "_Singleton");
                        _instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                        Debug.Log($"创建单例: {typeof(T)}");
                    }
                }
                return _instance;
            }
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if(global)
                DontDestroyOnLoad(gameObject);
            OnAwake();
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"检测到重复单例 {typeof(T)}，销毁新实例");
            Destroy(gameObject);
        }
    }

    protected virtual void OnAwake()
    {
    }

    protected virtual void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
        Destroy(gameObject);
    }
}