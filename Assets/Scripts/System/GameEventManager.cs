using System;
using System.Collections.Generic;

public enum GameEventType
{
    MonsterDead,
    SectionClear,
    GameClear,
    GameEnd,
    ScoreUpdated,
    SetActivePlayerInput,
}

public static class GameEventManager
{
    private static Dictionary<GameEventType, Action<object>> _eventDictionary = new Dictionary<GameEventType, Action<object>>();

    public static void Attach(GameEventType eventType, Action<object> action)
    {
        if (!_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType] = action;
        }
        else
        {
            _eventDictionary[eventType] += action;
        }
    }

    public static void Detach(GameEventType eventType, Action<object> action)
    {
        if (!_eventDictionary.ContainsKey(eventType))
        {
            return;
        }

        _eventDictionary[eventType] -= action;
    }

    public static void ClearAll()
    {
        _eventDictionary.Clear();
    }

    public static void TriggerEvent(GameEventType eventType, object param)
    {
        if (!_eventDictionary.ContainsKey(eventType))
        {
            return;
        }

        _eventDictionary[eventType]?.Invoke(param);
    }
}