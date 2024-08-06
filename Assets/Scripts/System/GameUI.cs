using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private GameObject _gameEndUI;

    public void Init()
    {
        _gameEndUI.SetActive(false);

        GameEventManager.Attach(GameEventType.GameClear, OnGameClear);
    }

    public void SetPlayerUI(PlayerController playerController)
    {
        _playerUI.SetPlayer(playerController);
    }

    private void OnGameClear(object sender)
    {
        _gameEndUI.SetActive(true);
    }
}
