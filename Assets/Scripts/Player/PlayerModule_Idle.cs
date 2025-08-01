using System.Collections;
using UnityEngine;

public class PlayerModule_Idle : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Idle;

    public override void Enter(PlayerState previousState)
    {
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.InputAxis.x != 0.0f || (player.StateModifier.State == PlayerModifiedState.Climbing && InputManager.Instance.InputAxis.y != 0.0f))
        {
            player.SetState(PlayerState.Move);
        }
    }
    public override void FixedUpdateState()
    {
    }

    public override void Exit(PlayerState nextState)
    {
    }
}