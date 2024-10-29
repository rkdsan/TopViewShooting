
public class PauseUI : PopupUI
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.TriggerEvent(EventType.SetActivePlayerInput, false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventManager.TriggerEvent(EventType.SetActivePlayerInput, true);
    }

    protected override void OnConfirmButton()
    {
        EventManager.TriggerEvent(EventType.GameEnd, null);
    }
}
