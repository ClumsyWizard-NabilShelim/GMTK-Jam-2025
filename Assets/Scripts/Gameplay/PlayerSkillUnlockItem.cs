using System.Collections;
using UnityEngine;

public class PlayerSkillUnlockItem : MonoBehaviour
{
    [SerializeField] private ControlRestriction unlockType;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerRestrictionManager.Instance.RemoveRestriction(unlockType);
        animator.SetBool("Show", true);
    }
}