using UnityEngine;

public class PlayerStateIdle : PlayerBaseState {
    public PlayerStateIdle(PlayerStateMachine stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory) { }

    public override void CheckSwitchState() {
        if (_ctx.InputReader.MoveInput != Vector2.zero) {
            SwitchState(_factory.Walk());
        }
        if (_ctx.InputReader.Dash) {
            SwitchState(_factory.Dash());
            _ctx.InputReader.ConsumeDashInput();
        }
        if (Input.GetMouseButtonDown(0)) {
            SwitchState(_factory.Shoot());
        }
    }

    public override void EnterState() {
        AnimatorStateInfo stateInfo = _ctx.Animator.GetCurrentAnimatorStateInfo(0);

        _ctx.Movement.Stop();
        _ctx.Sprite = "Idle";
    }

    public override void ExitState() {

    }

    public override void FixedUpdateState() {

    }

    public override void UpdateState() {
        CheckSwitchState();
    }
}
