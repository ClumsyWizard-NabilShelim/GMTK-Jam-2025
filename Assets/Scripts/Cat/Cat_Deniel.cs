using UnityEngine;

public class Cat_Deniel : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float wakeUpRange;
    [SerializeField] private float runAwayRange;
    [SerializeField] private LayerMask targetLayer;

    private bool wokeUp;
    private bool runningAway;

    private void Start()
    {
        wokeUp = false;
        runningAway = false;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!wokeUp)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, wakeUpRange, targetLayer);

            if (col != null)
            {
                wokeUp = true;
                animator.SetTrigger("GetUp");
            }
        }
        else if(!runningAway)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, runAwayRange, targetLayer);

            if (col != null)
            {
                runningAway = true;
                animator.SetTrigger("RunAway");
                Destroy(gameObject, 5.0f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wakeUpRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, runAwayRange);
    }
}