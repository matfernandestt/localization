using System.Linq;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            return _instance;
        }
    }

    public static T GetInstance(string name)
    {
        if (!_instance)
            _instance = (T)Resources.Load(name);
        return _instance;
    }
}
