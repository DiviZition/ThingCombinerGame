using R3;

public class PlayerStateData
{
    public StateType CurrentState { get; private set; }

    public readonly Subject<StateType> OnStateChange = new Subject<StateType>();

    public PlayerStateData()
    {
        CurrentState = StateType.Default;
    }

    public void SwitchState(StateType newState)
    {
        if(CurrentState != newState)
        {
            CurrentState = newState;
            OnStateChange.OnNext(newState);
        }
    }
    public enum StateType
    {
        Default,
        Swimming,
        Dialoguing,
        Trading,
    }
}