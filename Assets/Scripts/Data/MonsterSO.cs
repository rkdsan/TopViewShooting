using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/MonsterData")]
public class MonsterSO : ScriptableObject
{
    public string Character;
    public float AttackRange;
    public int MaxHP;
    public int AttackPower;
}
