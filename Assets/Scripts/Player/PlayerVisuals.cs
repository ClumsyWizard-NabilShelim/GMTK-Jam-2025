using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
public class PlayerVisuals : MonoBehaviour
{
    private Animator animator;
    private Player player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.State == PlayerState.Vault || player.StateModifier.IsInLockedState())
            return;

        UpdateFacing();
    }

    //Animation
    public void PlayWalk()
    {
        animator.SetFloat("MoveAmount", 0.5f);
    }
    public void PlayRun()
    {
        animator.SetFloat("MoveAmount", 1.0f);
    }
    public void StopMove()
    {
        animator.SetFloat("MoveAmount", -1.0f);
    }
    public void PlayPushPull(float dir)
    {
        animator.SetFloat("MoveAmount", 1.0f);
        animator.SetFloat("PushPullDir", dir);
    }

    public void Play(string name, bool play)
    {
        animator.SetBool(name, play);
    }
    public void Trigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void ActivateCrouchLayer()
    {
        animator.SetLayerWeight(0, 0.0f);
        animator.SetLayerWeight(1, 1.0f);
        animator.SetLayerWeight(2, 0.0f);
    }
    public void ActivateDragLayer()
    {
        animator.SetLayerWeight(0, 0.0f);
        animator.SetLayerWeight(1, 0.0f);
        animator.SetLayerWeight(2, 1.0f);
    }
    public void ActivateNormalLayer()
    {
        animator.SetLayerWeight(0, 1.0f);
        animator.SetLayerWeight(1, 0.0f);
        animator.SetLayerWeight(2, 0.0f);
    }

    public void PauseAnimator(bool pause)
    {
        animator.speed = pause ? 0.0f : 1.0f;
    }

    //Visuals
    private void UpdateFacing()
    {
        spriteRenderer.flipX = player.Facing.x < 0.0f;
    }
}