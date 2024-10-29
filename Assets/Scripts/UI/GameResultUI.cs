using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameResultUI : PopupUI
{
    protected override void OnConfirmButton()
    {
        EventManager.TriggerEvent(EventType.GameEnd, null);
    }

}
