using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameSection;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private List<SectionGroup> _sectionGroups;

    private int _currentSectionGroupIndex = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        GameEventManager.ClearAll();
        _gameUI.Init();
        SetPlayer();
        yield return WaitTimeManager.GetWaitTime(1);

        foreach (var section in _sectionGroups)
        {
            section.gameObject.SetActive(false);
        }

        _currentSectionGroupIndex = 0;
        StartSection(_currentSectionGroupIndex);
    }

    private void SetPlayer()
    {
        _player.Init();
        _gameUI.SetPlayerUI(_player);
    }

    private void StartSection(int sectionGroupIndex)
    {
        Debug.Log("섹션 그룹 스타트");
        var targetSection = _sectionGroups[sectionGroupIndex];

        targetSection.SectionClearEvent += OnClearSection;
        targetSection.ActiveSection(_player);
    }

    private void OnClearSection(GameSection section)
    {
        Debug.Log("섹션 클리어 받음");
        if (IsAllClear())
        {
            EndGame();
            return;
        }

        _currentSectionGroupIndex++;
        section.SectionClearEvent -= OnClearSection;

        StartSection(_currentSectionGroupIndex);
    }

    private bool IsAllClear()
    {
        return _currentSectionGroupIndex >= _sectionGroups.Count - 1;
    }

    private void EndGame()
    {
        Debug.Log("게임 클리어");
        var playerInput = _player.GetComponent<PlayerInput>();
        playerInput.enabled = false;

        GameEventManager.TriggerEvent(this, GameEventType.GameClear);
    }

}
