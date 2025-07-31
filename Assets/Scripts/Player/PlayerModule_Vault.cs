using UnityEngine;

public class PlayerModule_Vault : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Vault;

    [SerializeField] private float vaultTime;
    private float currentTime;
    private Vector2 startingPosition;
    private Vector2 targetPos1;
    private Vector2 targetPos2;
    private bool firstTarget;

    public override void Enter(PlayerState previousState)
    {
        firstTarget = true;
        targetPos1 = player.EdgeDetector.GetLedgeTopPos();
        targetPos2 = player.EdgeDetector.GetLedgeTopTargetPos();
        startingPosition = player.RB.position;
        player.ToggleGravity(false);
        currentTime = 0.0f;

        player.Visuals.Trigger("Vault");
    }

    public override void UpdateState()
    {
        if(currentTime <= vaultTime)
        {
            player.RB.position = Vector2.Lerp(startingPosition, firstTarget ? targetPos1 : targetPos2, currentTime / vaultTime);
            currentTime += Time.deltaTime;
        }
        else
        {
            if(firstTarget)
            {
                firstTarget = false;
                startingPosition = transform.position;
                currentTime = 0.0f;
                return;
            }

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