using UnityEngine;

public abstract class GameSection : MonoBehaviour
{
    public delegate void GameSectionDelegate(GameSection sender);
    public event GameSectionDelegate SectionClearEvent;

    public bool IsClear { get; private set; }
    public GameSectionSO SectionData { get; private set; }

    public void SetSectionData(GameSectionSO sectiondata)
    {
        SectionData = sectiondata;
    }

    public virtual void ActiveSection(PlayerController player) { }

    protected virtual void ClearSection()
    {
        IsClear = true;
        SectionClearEvent(this);
    }
}
