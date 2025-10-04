using UnityEngine;

public class PlayerStateWalk : PlayerBaseState {
    public PlayerStateWalk(PlayerStateMachine stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory) { }
    public override void CheckSwitchState() {
        if (_ctx.Body.linearVelocity == Vector2.zero && _ctx.Direction == Vector2.zero) {
            SwitchState(_factory.Idle());
        }
        if (_ctx.InputReader.Dash) {
            SwitchState(_factory.Dash());
            _ctx.InputReader.ConsumeDashInput();
        }
    }

    public override void EnterState() {
        _ctx.Sprite = "Walk";
        _ctx.MoveSpeed = _ctx.Stats.BaseSpeed;
    }

    public override void ExitState() {
        _ctx.MoveSpeed = 0;
        _ctx.Direction = Vector2.zero;
    }

    public override void FixedUpdateState() {
        _ctx.Movement.LinearMove();
    }

    public override void UpdateState() {
        _ctx.Direction = _ctx.InputReader.MoveInput;
        CheckSwitchState();
    }
}
