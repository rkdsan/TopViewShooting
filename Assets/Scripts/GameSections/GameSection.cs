using System;
using UnityEngine;

public abstract class GameSection : MonoBehaviour
{
    public delegate void GameSectionDelegate(GameSection sender);

    public bool IsClear { get; private set; }
    public event GameSectionDelegate SectionClearEvent;

    public abstract void ActiveSection(PlayerController player);

    protected virtual void ClearSection()
    {
        IsClear = true;
        SectionClearEvent(this);
    }
}
