using UnityEngine;

public class PlayerModule_Vault : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Vault;

    [SerializeField] private float height;
    [SerializeField] private float vaultTime1;
    [SerializeField] private float vaultTime2;
    [SerializeField] private float vaultTime3;
    private float currentVaultTime;
    private float currentTime;
    private Vector2 startingPosition;
    private Vector2 targetPos1;
    private Vector2 targetPos2;
    private Vector2 targetPos3;
    private bool firstTarget;
    private bool reachedHangingPoint;

    public override void Enter(PlayerState previousState)
    {
        reachedHangingPoint = false;
        firstTarget = true;
        currentVaultTime = vaultTime1;

        targetPos1 = player.EdgeDetector.GetLedgeTopPos() - Vector2.up * height;
        targetPos2 = player.EdgeDetector.GetLedgeTopPos();
        targetPos3 = player.EdgeDetector.GetLedgeTopTargetPos();

        startingPosition = player.RB.position;
        player.ToggleGravity(false);
        player.RB.linearVelocity = Vector2.zero;
        currentTime = 0.0f;

        player.Visuals.Trigger("Vault");
    }

    public override void UpdateState()
    {
        if(currentTime <= currentVaultTime)
        {
            player.RB.position = Vector2.Lerp(startingPosition, !reachedHangingPoint ? targetPos1 : (firstTarget ? targetPos2 : targetPos3), currentTime / currentVaultTime);
            currentTime += Time.deltaTime;
        }
        else
        {
            if(!reachedHangingPoint)
            {
                reachedHangingPoint = true;
                currentVaultTime = vaultTime2;
                startingPosition = transform.position;
                currentTime = 0.0f;
                return;
            }
            if(firstTarget)
            {
                firstTarget = false;
                currentVaultTime = vaultTime3;
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
