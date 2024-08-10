
public class PauseUI : PopupUI
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GameEventManager.TriggerEvent(GameEventType.SetActivePlayerInput, false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEventManager.TriggerEvent(GameEventType.SetActivePlayerInput, true);
    }

    protected override void OnConfirmButton()
    {
        GameEventManager.TriggerEvent(GameEventType.GameEnd, null);
    }
}
