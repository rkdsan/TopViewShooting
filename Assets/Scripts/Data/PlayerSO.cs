using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/PlayerData")]
public class PlayerSO : ScriptableObject
{
    public int MaxHP;
    public float MoveSpeed;
    public WeaponSO Weapon;
}
