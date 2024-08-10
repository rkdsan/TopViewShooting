using System;
public class PlayerModel
{
    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }
    public float MoveSpeed { get; private set; }

    public PlayerModel(PlayerSO playerData)
    {
        CurrentHP = MaxHP = playerData.MaxHP;
        MoveSpeed = playerData.MoveSpeed;
    }

    public void TakeDamage(int damage)
    {
        int newHP = CurrentHP - damage;
        CurrentHP = Math.Max(0, newHP);
    }
}
