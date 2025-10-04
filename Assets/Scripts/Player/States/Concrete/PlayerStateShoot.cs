using UnityEditor;
using UnityEngine;

public class PlayerStateShoot : PlayerBaseState {
    Vector3 _direction;
    bool _shot = false;
    public PlayerStateShoot(PlayerStateMachine stateMachine, PlayerStateFactory stateFactory) : base(stateMachine, stateFactory) { }
    public override void CheckSwitchState() {
        if(!Input.GetMouseButton(0) && !_shot) {
            _shot = true;
            _ctx.Animator.speed = 1f;
        }
        AnimatorStateInfo stateInfo = _ctx.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f && _shot) {
            _ctx.Shoot(_direction);
            SwitchState(_factory.Idle());
        }
    }

    public override void EnterState() {
        _ctx.Sprite = "Shoot";
        _ctx.MoveSpeed = 0;
        _ctx.Animator.speed = 0f;
    }

    public override void ExitState() {
        _ctx.MoveSpeed = 0;
        _ctx.Direction = Vector2.zero;
    }

    public override void FixedUpdateState() {
        _ctx.Movement.LinearMove();
    }

    public override void UpdateState() {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 dir = (mousePosition - _ctx.gameObject.transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        float snappedAngle = Mathf.Round(angle / 45f) * 45f;

        _direction = new Vector3(
            Mathf.Cos(snappedAngle * Mathf.Deg2Rad),
            Mathf.Sin(snappedAngle * Mathf.Deg2Rad)
        );

        _ctx.Direction = _direction;

        CheckSwitchState();
    }
}
