using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemManager : MonoBehaviour
{
    public static EventSystemManager Instance;

    private Dictionary<string, Action> eventActions = new Dictionary<string, Action>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // S'abonner à un événement
    public void SubscribeToEvent(string eventName, Action action)
    {
        if (!eventActions.ContainsKey(eventName))
        {
            eventActions[eventName] = action;
        }
        else
        {
            eventActions[eventName] += action;
        }
    }

    // Déclencher un événement
    public void TriggerEvent(string eventName)
    {
        if (eventActions.ContainsKey(eventName))
        {
            eventActions[eventName]?.Invoke();
        }
    }
}

