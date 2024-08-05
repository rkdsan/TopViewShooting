using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI;

    public void SetPlayerUI(PlayerController playerController)
    {
        _playerUI.SetPlayer(playerController);
    }
}
