using System;
using System.Collections.Generic;

public enum GameEventType
{
    MonsterDead,
    SectionClear,
    SectionGroupClear,
}

public static class GameEventManager
{
    public static Dictionary<GameEventType, Dictionary<object, Action<object>>> eventDictionary 
            = new Dictionary<GameEventType, Dictionary<object, Action<object>>>();

    public static void Attach(GameEventType eventType, object subject, Action<object> action)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = new Dictionary<object, Action<object>>();
        }

        if (!eventDictionary[eventType].ContainsKey(subject))
        {
            eventDictionary[eventType][subject] = action;
        }
        else
        {
            eventDictionary[eventType][subject] += action;
        }
    }

    public static void Detach(GameEventType eventType, object subject, Action<object> action)
    {
        if (!HasKey(eventType, subject))
            return;

        eventDictionary[eventType][subject] -= action;
    }

    public static void TriggerEvent(GameEventType eventType, object subject)
    {
        if (!HasKey(eventType, subject))
            return;

        eventDictionary[eventType][subject]?.Invoke(subject);
    }

    private static bool HasKey(GameEventType eventType, object subject)
    {
        if (!eventDictionary.ContainsKey(eventType))
            return false;

        if (!eventDictionary[eventType].ContainsKey(subject))
            return false;

        return true;
    }

}