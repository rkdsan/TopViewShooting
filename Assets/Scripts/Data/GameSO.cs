using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Object/GameData")]
public class GameSO : ScriptableObject
{
    public PlayerSO Player;
    public List<GameSectionSO> SectionList;
}
