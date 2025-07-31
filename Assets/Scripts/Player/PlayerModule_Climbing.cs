using UnityEngine;

public class PlayerModule_Climbing : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Climb;
    [SerializeField] private float climbSpeed;

    public override void Enter(PlayerState previousState)
    {
        player.Visuals.Play("Climb", true);
        player.ToggleGravity(false);
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.InputAxis.y == 0.0f)
            player.Visuals.PauseAnimator(true);
        else
            player.Visuals.PauseAnimator(false);
    }

    public override void FixedUpdateState()
    {
        player.RB.linearVelocity = InputManager.Instance.InputAxis * climbSpeed;
    }

    public override void Exit(PlayerState nextState)
    {
        player.ToggleGravity(true);
        player.Visuals.PauseAnimator(false);
        player.Visuals.Play("Climb", false);
    }
}