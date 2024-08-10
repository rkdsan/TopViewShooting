using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectionGroup : GameSection
{
    [SerializeField] private List<GameSection> sections;

    public override void ActiveSection(PlayerController player)
    {
        var data = (SectionGroupSO)SectionData;

        gameObject.SetActive(true);
        for(int i =  0; i < sections.Count; i++)
        {
            var section = sections[i];
            section.SectionClearEvent += OnClearSection;
            section.SetSectionData(data.SectionList[i]);
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
