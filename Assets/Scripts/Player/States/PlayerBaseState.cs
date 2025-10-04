public abstract class PlayerBaseState {
    protected PlayerStateMachine _ctx;
    protected PlayerStateFactory _factory;

    public PlayerBaseState(PlayerStateMachine stateMachine, PlayerStateFactory stateFactory) {
        _ctx = stateMachine;
        _factory = stateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    protected void SwitchState(PlayerBaseState newState) {
        // call the exit state method of the current state
        ExitState();

        // then call the enter state of next state
        newState.EnterState();

        // switch current state of context
        _ctx.CurrentState = newState;


    }
}
