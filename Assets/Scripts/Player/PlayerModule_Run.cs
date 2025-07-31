using System.Collections;
using UnityEngine;


public class PlayerModule_Run : PlayerStateModule
{
    [SerializeField] private float moveSpeed;
    public override PlayerState TargetState => PlayerState.Run;

    public override void Enter(PlayerState previousState)
    {
        player.Visuals.PlayRun(true);
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.InputAxis.x == 0.0f)
            player.SetState(PlayerState.Idle);

    }
    public override void FixedUpdateState()
    {
        player.RB.linearVelocityX = InputManager.Instance.InputAxis.x * moveSpeed;
    }

    public override void Exit(PlayerState nextState)
    {
        player.RB.linearVelocityX = 0.0f;
        player.Visuals.PlayRun(false);
    }
}