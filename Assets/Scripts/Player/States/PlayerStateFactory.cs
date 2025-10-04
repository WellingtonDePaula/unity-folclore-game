public class PlayerStateFactory {
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine stateMachine) {
        _context = stateMachine;
    }

    public PlayerStateIdle Idle() {
        return new PlayerStateIdle(_context, this);
    }
    public PlayerStateWalk Walk() {
        return new PlayerStateWalk(_context, this);
    }
    public PlayerStateDash Dash() {
        return new PlayerStateDash(_context, this);
    }
    public PlayerStateShoot Shoot() {
        return new PlayerStateShoot(_context, this);
    }
}
