using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/WeaponData")]
public class WeaponSO : ScriptableObject
{
    public string WeaponType;
    public int AttackRange;
}
