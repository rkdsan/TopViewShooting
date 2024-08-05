using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private List<SectionGroup> _sectionGroups;

    private int _currentSectionGroupIndex = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        GameStart();
    }

    private void GameStart()
    {
        _gameUI.SetPlayerUI(_player);
        _currentSectionGroupIndex = 0;
        StartSection(_currentSectionGroupIndex);
    }

    private void StartSection(int sectionGroupIndex)
    {
        var targetSection = _sectionGroups[sectionGroupIndex];

        GameEventManager.Attach(GameEventType.SectionClear, targetSection, OnClearSectionGroup);
        targetSection.ActiveSection(_player);
    }

    private void OnClearSectionGroup(object eventEmitter)
    {
        if (IsAllClear())
        {
            EndGame();
            return;
        }

        _currentSectionGroupIndex++;
        GameEventManager.Detach(GameEventType.SectionClear, eventEmitter, OnClearSectionGroup);

        StartSection(_currentSectionGroupIndex);
    }

    private bool IsAllClear()
    {
        return _currentSectionGroupIndex >= _sectionGroups.Count - 1;
    }

    private void EndGame()
    {
        Debug.Log("게임 클리어");
    }

}
