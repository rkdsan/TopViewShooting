using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionGroup : GameSection
{
    [SerializeField] private List<GameSection> sections;

    public override void ActiveSection(PlayerController player)
    {
        gameObject.SetActive(true);
        foreach (var section in sections)
        {
            section.SectionClearEvent += OnClearSection;
            section.ActiveSection(player);
        }
    }

    private void OnClearSection(GameSection sender)
    {
        sender.SectionClearEvent -= OnClearSection;

        bool isAllClear = sections.All(t => t.IsClear);
        if (isAllClear)
        {
            ClearSection();
        }
    }
}
