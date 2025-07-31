using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerModule_Idle : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Idle;

    public override void Enter(PlayerState previousState)
    {
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.InputAxis.x != 0.0f)
        {
            if(player.IsRunning)
                player.SetState(PlayerState.Run);
            else
                player.SetState(PlayerState.Walk);
        }
            
    }
    public override void FixedUpdateState()
    {
    }

    public override void Exit(PlayerState nextState)
    {
    }
}