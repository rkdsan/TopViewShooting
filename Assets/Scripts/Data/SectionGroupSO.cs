using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SectionGroupData", menuName = "Scriptable Object/SectionGroupData")]
public class SectionGroupSO : GameSectionSO
{
    public List<GameSectionSO> SectionList;
}

