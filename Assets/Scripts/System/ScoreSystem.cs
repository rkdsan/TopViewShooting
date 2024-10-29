using System;

public class ScoreSystem
{
    public int GameScore { get; private set; }

    public void Init()
    {
        GameScore = 0;

        EventManager.Subcribe(EventType.MonsterDead, OnMonsterDead);
        EventManager.Subcribe(EventType.SectionClear, OnSectionClear);
    }
    
    private void AddScore(int addValue)
    {
        GameScore += addValue;
        GameScore = Math.Max(0, GameScore);
        EventManager.TriggerEvent(EventType.ScoreUpdated, GameScore);
    }

    private void OnMonsterDead(object param)
    {
        AddScore(12);
    }

    private void OnSectionClear(object param)
    {
        AddScore(123);
    }

}
