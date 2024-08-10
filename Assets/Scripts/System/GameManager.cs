using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSO _gameData;

    [SerializeField] private PlayerController _player;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private List<SectionGroup> _sectionGroups;

    private ScoreSystem _scoreSystem;
    private int _currentSectionGroupIndex = 0;

    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        GameEventManager.ClearAll();
        GameEventManager.Attach(GameEventType.GameEnd, EndGame);

        _scoreSystem = new ScoreSystem();

        _gameUI.Init();
        SetData(_gameData);
        yield return WaitTimeManager.GetWaitTime(1);

        _currentSectionGroupIndex = 0;
        StartSection(_currentSectionGroupIndex);
    }

    private void SetData(GameSO gameData)
    {
        _player.Init(gameData.Player);
        _gameUI.SetPlayerUI(_player);

        for(int i = 0; i < _sectionGroups.Count; i++)
        {
            var section = _sectionGroups[i];
            section.SetSectionData(gameData.SectionList[i]);
        }
    }

    private void StartSection(int sectionGroupIndex)
    {
        Debug.Log("섹션 그룹 시작");
        var targetSection = _sectionGroups[sectionGroupIndex];

        targetSection.SectionClearEvent += OnClearSection;
        targetSection.ActiveSection(_player);
    }

    private void OnClearSection(GameSection section)
    {
        Debug.Log("섹션 클리어 받음");
        if (IsAllClear())
        {
            ClearGame();
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

    private void ClearGame()
    {
        Debug.Log("게임 클리어");
        GameEventManager.TriggerEvent(GameEventType.SetActivePlayerInput, false);
        GameEventManager.TriggerEvent(GameEventType.GameClear, this);
    }

    private void EndGame(object param)
    {
        SceneManager.LoadScene("TitleScene");
    }
}
