using System;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public enum EventType
{
    MonsterDead,
    SectionClear,
    GameClear,
    GameEnd,
    ScoreUpdated,
    SetActivePlayerInput,
}

public static class EventManager
{
    private static Dictionary<EventType, Action<object>> _eventDictionary = new Dictionary<EventType, Action<object>>();

    //오브젝트가 재사용 될 때 object를 키로 찾아서 초기화하기 위해 object를 첫번째 키로 설정
    private static Dictionary<object, Dictionary<EventType, Action<object>>> _targetEventDictionary = new Dictionary<object, Dictionary<EventType, Action<object>>>();
    

    public static void Subcribe(EventType eventType, Action<object> action)
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

    public static void Subcribe(object target, EventType eventType, Action<object> action)
    {
        if (!_targetEventDictionary.ContainsKey(target))
        {
            _targetEventDictionary[target] = new Dictionary<EventType, Action<object>>();
            _targetEventDictionary[target][eventType] = action;
        }
        else
        {
            _targetEventDictionary[target][eventType] += action;
        }
    }

    public static void Unsubscribe(EventType eventType, Action<object> action)
    {
        if (_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType] -= action;
        }
    }

    public static void Unsubscribe(object target, EventType eventType, Action<object> action)
    {
        if (_targetEventDictionary.ContainsKey(target) && _targetEventDictionary[target].ContainsKey(eventType))
        {
            _targetEventDictionary[target][eventType] -= action;
        }
    }

    public static void ClearAll()
    {
        _eventDictionary.Clear();
        _targetEventDictionary.Clear();
    }

    public static void ClearTargetEvent(object target)
    {
        if (_targetEventDictionary.ContainsKey(target))
        {
            _targetEventDictionary[target].Clear();
        }
    }

    public static void TriggerEvent(EventType eventType, object sender)
    {
        if (_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType]?.Invoke(sender);
        }

        if (_targetEventDictionary.ContainsKey(sender) && _targetEventDictionary[sender].ContainsKey(eventType))
        {
            _targetEventDictionary[sender][eventType]?.Invoke(sender);
        }
    }
}