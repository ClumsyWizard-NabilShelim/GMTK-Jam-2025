using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Patrol")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Vector2> patrolPoints;
    private int currentIndex;

    [Header("Combat")]
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private float attackDuration;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;
    private float currentTime;
    private Player player;
    private bool isAttacking;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        }
        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
                Attack();
        }
    }

    private void Patrol()
    {
        if(Vector2.Distance(transform.position, patrolPoints[currentIndex]) <= 0.1f)
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
    }
    
    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);

        if (col == null)
            return;

    //    player.Stats.Damage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0;i < patrolPoints.Count;i++)
        {
            if(Application.isPlaying)
                Gizmos.DrawSphere(patrolPoints[i], 0.1f);
            else
                Gizmos.DrawSphere(patrolPoints[i] + (Vector2)transform.position, 0.1f);
        }
    }
}