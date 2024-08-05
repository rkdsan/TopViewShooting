using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Attack();
}

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract void Attack();
}
