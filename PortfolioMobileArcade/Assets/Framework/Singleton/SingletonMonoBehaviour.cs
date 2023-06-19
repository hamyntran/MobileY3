using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton) " + typeof(T).ToString();

                }
                
            }
            

            return _instance;
        }
    }

    public virtual void Awake()
    {
        T[] admods = GameObject.FindObjectsOfType<T>();
        if (admods.Length > 1)
            for (int i = 1; i < admods.Length; i++)
            {
                Destroy(admods[i].gameObject);
            }
    }
}