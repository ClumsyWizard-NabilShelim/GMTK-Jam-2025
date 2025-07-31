using UnityEngine;

public class PlayerModule_Combat : PlayerStateModule
{
    public override PlayerState TargetState => PlayerState.Combat;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private float damageRadius;
    [SerializeField] private float attackDuration;
    [SerializeField] private float damageDelay;

    public override void Enter(PlayerState previousState)
    {
        player.Visuals.Trigger("Attack");
        Invoke("EndCombat", attackDuration);
        Invoke("Damage", damageDelay);
    }

    public override void UpdateState()
    {
    }
    public override void FixedUpdateState()
    {
    }

    private void EndCombat()
    {
        player.SetState(PlayerState.Idle);
    }

    private void Damage()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, damageRadius, damageLayer);
        if (cols == null || cols.Length == 0)
            return;

        for (int i = 0; i < cols.Length; i++)
        {
            if (Vector2.Dot(player.Facing, (cols[i].transform.position - transform.position)) > 0.0f)
            {
                CW_CameraShakeManager.Instance.Shake(CameraShakeType.Low);
                cols[i].GetComponent<IDamageable>().Damage(damage);
            }
        }
    }

    public override void Exit(PlayerState nextState)
    {
    }

    //Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}