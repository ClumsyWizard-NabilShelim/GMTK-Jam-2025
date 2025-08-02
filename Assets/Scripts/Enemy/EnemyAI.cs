using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Patrol")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Vector2> patrolPoints;
    private int currentIndex;

    [Header("Combat")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;
    private float currentTime;
    private Player player;
    private bool isAttacking;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentTime = timeBetweenAttack;

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            patrolPoints[i] += (Vector2)transform.position;
        }
    }

    private void Update()
    {
        if (player == null)
        {
            Patrol();
            Detect();
        }
        else
        {
            if (isAttacking)
            {
                rb.linearVelocityX = 0.0f;
            }
            else
            {
                if (currentTime > 0.0f)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
                        Attack();
                    else
                        rb.linearVelocityX = (player.transform.position - transform.position).normalized.x * moveSpeed;
                }
            }
        }

        if (rb.linearVelocityX < 0.0f)
            spriteRenderer.flipX = true;
        else if (rb.linearVelocityX > 0.0f)
            spriteRenderer.flipX = false;

        animator.SetBool("Move", rb.linearVelocityX != 0.0f);
    }

    private void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolPoints[currentIndex]) <= 0.5f)
        {
            currentIndex++;
            if(currentIndex >= patrolPoints.Count)
                currentIndex = 0;
        }
        else
        {
            rb.linearVelocityX = (patrolPoints[currentIndex] - (Vector2)transform.position).normalized.x * moveSpeed;
        }
    }
    private void Detect()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (col == null)
            return;

        player = col.GetComponent<Player>();
        if (player.Stats.IsDead)
            player = null;
    }
    
    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        currentTime = timeBetweenAttack;
    }

    private void DamageTarget()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (col == null)
            return;

        CW_CameraShakeManager.Instance.Shake(CameraShakeType.Low);
        player.Stats.Damage(damage);
    }

    private void AttackOver()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        for (int i = 0;i < patrolPoints.Count;i++)
        {
            if(Application.isPlaying)
                Gizmos.DrawSphere(patrolPoints[i], 0.1f);
            else
                Gizmos.DrawSphere(patrolPoints[i] + (Vector2)transform.position, 0.1f);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}