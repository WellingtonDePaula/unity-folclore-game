using UnityEngine;

public class PlayerMovement {
    PlayerStateMachine _ctx;

    public PlayerMovement(PlayerStateMachine context) {
        _ctx = context;
    }

    public void LinearMove() {
        _ctx.Body.linearVelocity = _ctx.Direction * Time.fixedDeltaTime * _ctx.MoveSpeed;
    }
    public void SmoothMove() {
        _ctx.Body.AddForce(_ctx.Direction * Time.fixedDeltaTime * _ctx.MoveSpeed);
    }

    public void Stop() {
        _ctx.Body.linearVelocity = Vector2.zero;
    }
}
