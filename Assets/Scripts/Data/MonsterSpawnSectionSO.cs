using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSectionData", menuName = "Scriptable Object/MonsterSectionData")]
public class MonsterSpawnSectionSO : GameSectionSO
{
    public List<MonsterSO> MonsterList;
}
