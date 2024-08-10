using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private PopupUI _endUI;
    [SerializeField] private PopupUI _pauseUI;

    [SerializeField] private Button _pauseButton;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() => _pauseUI.Open());
    }

    public void Init()
    {
        _endUI.gameObject.SetActive(false);
        _pauseUI.gameObject.SetActive(false);
        
        GameEventManager.Attach(GameEventType.GameClear, OnGameClear);
    }

    public void SetPlayerUI(PlayerController playerController)
    {
        _playerUI.SetPlayer(playerController);
    }

    private void OnGameClear(object sender)
    {
        _endUI.Open();
    }
}
