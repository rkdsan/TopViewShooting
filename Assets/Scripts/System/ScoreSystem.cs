using System;

public class ScoreSystem
{
    public int GameScore { get; private set; }

    public void Init()
    {
        GameScore = 0;

        GameEventManager.Attach(GameEventType.MonsterDead, OnMonsterDead);
        GameEventManager.Attach(GameEventType.SectionClear, OnSectionClear);
    }
    
    private void AddScore(int addValue)
    {
        GameScore += addValue;
        GameScore = Math.Max(0, GameScore);
        GameEventManager.TriggerEvent(GameEventType.ScoreUpdated, GameScore);
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
