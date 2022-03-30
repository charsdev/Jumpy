using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public static class EventManager
{
    private static Dictionary<string, EventHandler<EventArgs>> eventDictionary = new Dictionary<string, EventHandler<EventArgs>>();

    public static void StartListening(string eventName, EventHandler<EventArgs> listener)
    {
        EventHandler<EventArgs> OnEvent;
        if (eventDictionary.TryGetValue(eventName, out OnEvent))
        {
            OnEvent += listener;
            eventDictionary[eventName] = OnEvent;
        }
        else
        {
            OnEvent += listener;
            eventDictionary.Add(eventName, OnEvent);
        }
    }

    public static void StopListening(string eventName, EventHandler<EventArgs> listener)
    {
        EventHandler<EventArgs> OnEvent;
        if (eventDictionary.TryGetValue(eventName, out OnEvent))
        {
            OnEvent -= listener;
            eventDictionary[eventName] = OnEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventArgs e, object sender)
    {
        EventHandler<EventArgs> OnEvent;
        if (eventDictionary.TryGetValue(eventName, out OnEvent))
        {
            OnEvent.Invoke(sender, e);
        }
    }

}
