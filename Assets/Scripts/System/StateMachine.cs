public class StateMachine<T>
{
    private T _owner;
    private IState<T> _curState;

    public StateMachine(T owner)
    {
        _owner = owner;
    }

    public void ChangeState(IState<T> newState)
    {
        if(_curState != null)
        {
            _curState.OnExit(_owner);
        }

        _curState = newState;
        _curState.OnEnter(_owner);
    }

    public void Update()
    {
        if(_curState != null)
        {
            _curState.OnUpdate(_owner);
        }
    }
}
