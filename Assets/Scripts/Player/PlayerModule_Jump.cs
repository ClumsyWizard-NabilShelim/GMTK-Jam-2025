using UnityEngine;

public class PlayerModule_Jump : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Jump;

    [SerializeField] private float airSpeed;
    [SerializeField] private float airControl;
    [SerializeField] private float jumpForce;
    [SerializeField] private float landDuration;
    private bool leftGround;
    private bool landing;

    public override void Enter(PlayerState previousState)
    {
        leftGround = false;
        landing = false;
        player.Visuals.Play("Jump", true);
        player.RB.linearVelocity = Vector2.zero;
        player.RB.AddForce(new Vector2(InputManager.Instance.InputAxis.x, 1.0f) * jumpForce, ForceMode2D.Impulse);
    }

    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {
        if (player.IsGrounded())
        {
            if (!leftGround || landing)
                return;

            leftGround = false;
            landing = true;
            player.Visuals.Play("Jump", false);
            player.RB.linearVelocityX = 0.0f;
            Invoke("LandComplete", landDuration);
        }
        else
        {
            leftGround = true;
            player.RB.linearVelocityX = Mathf.Lerp(player.RB.linearVelocityX, InputManager.Instance.InputAxis.x * airSpeed, airControl * Time.fixedDeltaTime);
        }
    }

    private void LandComplete()
    {
        player.SetState(PlayerState.Idle);
    }

    public override void Exit(PlayerState nextState)
    {
        player.Visuals.Play("Jump", false);
    }
}