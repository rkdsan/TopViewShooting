using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionGroup : GameSection
{
    [SerializeField] private List<GameSection> sections;

    public override void ActiveSection(PlayerController player)
    {
        foreach (var section in sections)
        {
            GameEventManager.Attach(GameEventType.SectionClear, section, OnClearSection);
            section.ActiveSection(player);
        }
    }

    private void OnClearSection(object eventEmitter)
    {
        Debug.Log("Section Group에서 Section Clear 받음");

        bool isAllClear = sections.All(t => t.IsClear);
        if (isAllClear)
        {
            ClearSection();
        }
    }
}
