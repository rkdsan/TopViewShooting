using System;
using UnityEngine;

public abstract class GameSection : MonoBehaviour
{
    public bool IsClear { get; private set; }

    public abstract void ActiveSection(PlayerController player);

    protected virtual void ClearSection()
    {
        IsClear = true;
        GameEventManager.TriggerEvent(GameEventType.SectionClear, this);
    }
}
