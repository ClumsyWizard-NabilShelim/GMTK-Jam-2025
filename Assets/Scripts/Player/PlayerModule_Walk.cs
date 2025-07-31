using System.Collections;
using UnityEngine;

public class PlayerModule_Walk : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Walk;
    [SerializeField] private float moveSpeed;

    public override void Enter(PlayerState previousState)
    {
        player.Visuals.PlayMove(true);
    }

    public override void UpdateState()
    {
        if(InputManager.Instance.InputAxis.x == 0.0f)
            player.SetState(PlayerState.Idle);

    }
    public override void FixedUpdateState()
    {
        player.RB.linearVelocityX = InputManager.Instance.InputAxis.x * (player.IsCrouching ? moveSpeed / 1.5f : moveSpeed);
    }

    public override void Exit(PlayerState nextState)
    {
        player.RB.linearVelocityX = 0.0f;
        player.Visuals.PlayMove(false);
    }
}