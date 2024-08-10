using System;
using UnityEngine;

public interface IMonsterModelListener
{
    void OnChangeHP(int currentHP, int maxHP);
    void OnDead();
}

public class MonsterModel
{
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public float AttackRange { get; private set; }
    public int AttackPower { get; private set; }
    public bool IsAlive { get; private set; }

    private IMonsterModelListener _monsterModelListener;

    public MonsterModel(MonsterSO monsterData, IMonsterModelListener listener)
    {
        IsAlive = true;
        _monsterModelListener = listener;
        MaxHP = monsterData.MaxHP;
        SetHP(MaxHP);
        AttackRange = monsterData.AttackRange;
        AttackPower = monsterData.AttackPower;
    }

    public void TakeDamage(int damage)
    {
        int newHP = CurrentHP - damage;
        CurrentHP = Math.Max(0, newHP);
        SetHP(newHP);
    }

    private void SetHP(int newHP)
    {
        CurrentHP = newHP;
        _monsterModelListener.OnChangeHP(CurrentHP, MaxHP);

        if(CurrentHP <= 0)
        {
            IsAlive = false;
            _monsterModelListener.OnDead();
        }
    }
}
