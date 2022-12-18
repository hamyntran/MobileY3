using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    public static EventDispatcher Instance;
    private Dictionary<string, Action<object>> events = new Dictionary<string, Action<object>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(this);
    }
   
   
    public void RegisterListener(string eventName, Action<object> callback)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] += callback;
        }
        else
        {
            events.Add(eventName,callback);
        }
    }

    public void RemoveListener(string eventName, Action<object> callback)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] -= callback;
        }
    }
   
    public void TriggerEvent(string eventName, object data = null)
    {
        if (events.ContainsKey(eventName))
        {
            if (events[eventName] != null)
            {
                events[eventName].Invoke(data);
            }
            else
            {
                events.Remove(eventName);
            }
        }
    }
}
