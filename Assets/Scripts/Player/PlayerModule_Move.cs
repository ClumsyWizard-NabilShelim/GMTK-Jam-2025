using System.Collections;
using UnityEngine;

public class PlayerModule_Move : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Move;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float dragSpeed;
    private float currentSpeed;


    public override void Initialize(Player player)
    {
        base.Initialize(player);

        player.StateModifier.OnStatModified += SetupAnimationAndSpeed;
    }

    public override void Enter(PlayerState previousState)
    {
        SetupAnimationAndSpeed();
    }

    public override void UpdateState()
    {
        if(InputManager.Instance.InputAxis.x == 0.0f && player.StateModifier.State != PlayerModifiedState.Climbing)
            player.SetState(PlayerState.Idle);

        if (player.StateModifier.State == PlayerModifiedState.Dragging)
        {
            player.Visuals.ToggleBackwards(false);
        }
        else
        {
            if (Mathf.Sign(InputManager.Instance.InputAxis.x) != Mathf.Sign(player.Facing.x))
                player.Visuals.ToggleBackwards(true);
            else
                player.Visuals.ToggleBackwards(false);
        }
    }
    public override void FixedUpdateState()
    {
        if(player.StateModifier.State == PlayerModifiedState.Climbing)
        {
            if (InputManager.Instance.InputAxis.y == 0.0f)
                player.Visuals.PauseAnimator(true);
            else
                player.Visuals.PauseAnimator(false);

            player.RB.linearVelocity = InputManager.Instance.InputAxis * currentSpeed;
        }
        if (player.StateModifier.State == PlayerModifiedState.Dragging)
        {
            player.Visuals.PlayPushPull(InputManager.Instance.InputAxis.x * player.Facing.x);
            print("Dragging");
            player.RB.linearVelocityX = InputManager.Instance.InputAxis.x * currentSpeed;
        }
        else
        {
            player.RB.linearVelocityX = InputManager.Instance.InputAxis.x * currentSpeed;
        }
    }

    public override void Exit(PlayerState nextState)
    {
        player.Visuals.ToggleBackwards(false);
        player.RB.linearVelocityX = 0.0f;
        player.Visuals.StopMove();
    }

    //Helper Functions
    private void SetupAnimationAndSpeed()
    {
        if (player.State != TargetState)
            return;

        if (player.StateModifier.State != PlayerModifiedState.Climbing)
        {
            player.ToggleGravity(true);
            player.Visuals.Play("Climb", false);
            player.Visuals.PauseAnimator(false);
        }

        switch (player.StateModifier.State)
        {
            case PlayerModifiedState.None:
                currentSpeed = walkSpeed;
                player.Visuals.PlayWalk();
                break;
            case PlayerModifiedState.Running:
                currentSpeed = runSpeed;
                player.Visuals.PlayRun();
                break;
            case PlayerModifiedState.Crouching:
                currentSpeed = crouchSpeed;
                player.Visuals.PlayWalk();
                break;
            case PlayerModifiedState.Dragging:
                currentSpeed = dragSpeed;
                player.Visuals.StopMove();
                break;
            case PlayerModifiedState.Climbing:
                player.ToggleGravity(false);
                currentSpeed = climbSpeed;
                player.Visuals.StopMove();
                player.Visuals.Play("Climb", true);
                break;
            default:
                break;
        }
    }
}