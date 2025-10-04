using UnityEditor;
using UnityEngine;

public class PlayerStateDash : PlayerBaseState {
    public PlayerStateDash(PlayerStateMachine stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory) { }
    public override void CheckSwitchState() {
        AnimatorStateInfo stateInfo = _ctx.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f) {
            SwitchState(_factory.Idle());
        }
    }

    public override void EnterState() {
        _ctx.Sprite = "Dash";
        _ctx.MoveSpeed = _ctx.Stats.BaseDashSpeed;

        _ctx.PlayAnimation();

        Vector2 dashDir = new Vector2();

        if (_ctx.InputReader.MoveInput != Vector2.zero) {
            dashDir = _ctx.InputReader.MoveInput;
        } else {
            if (_ctx.Orientation == "Side") {
                dashDir = new Vector2(Mathf.Sign(_ctx.transform.localScale.x), 0);
            } else if (_ctx.Orientation == "Front") {
                dashDir = new Vector2(0, -1);
            } else { dashDir = new Vector2(0, 1); }
        }

        _ctx.Direction = dashDir;
    }

    public override void ExitState() {
        _ctx.MoveSpeed = 0;
    }

    public override void FixedUpdateState() {
        _ctx.MoveSpeed = Mathf.Lerp(_ctx.MoveSpeed, 0, Time.fixedDeltaTime);
        _ctx.Movement.LinearMove();
    }

    public override void UpdateState() {
        CheckSwitchState();
    }
}
