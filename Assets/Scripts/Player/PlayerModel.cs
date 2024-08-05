using System;
public class PlayerModel
{
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }

    public PlayerModel()
    {
        CurrentHP = MaxHP = 100;
    }

    public void TakeDamage(int damage)
    {
        int newHP = CurrentHP - damage;
        CurrentHP = Math.Max(0, newHP);
    }
}
