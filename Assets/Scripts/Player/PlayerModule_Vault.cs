using UnityEngine;

public class PlayerModule_Vault : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Vault;

    [SerializeField] private float vaultTime;
    private float currentTime;
    private Vector2 startingPosition;
    private Vector2 targetPos;

    public override void Enter(PlayerState previousState)
    {
        targetPos = player.EdgeDetector.GetLedgeTopTargetPos();

        startingPosition = player.RB.position;
        player.ToggleGravity(false);
        player.RB.linearVelocity = Vector2.zero;
        currentTime = 0.0f;

        player.Visuals.Trigger("Vault");
    }

    public override void UpdateState()
    {
        if(currentTime <= vaultTime)
        {
            player.RB.position = Vector2.Lerp(startingPosition, targetPos, currentTime / vaultTime);
            currentTime += Time.deltaTime;
        }
        else
        {
            player.SetState(PlayerState.Idle);
        }
    }
    public override void FixedUpdateState()
    {

    }

    public override void Exit(PlayerState nextState)
    {
        player.ToggleGravity(true);
    }
}
