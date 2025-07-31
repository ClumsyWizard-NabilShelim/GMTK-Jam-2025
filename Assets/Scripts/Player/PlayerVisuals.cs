using System.Collections;
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
        if (player.State == PlayerState.Vault)
            return;

        UpdateFacing();
    }

    //Animation
    public void PlayMove(bool play)
    {
        if (play)
            animator.SetFloat("MoveAmount", 0.5f);
        else
            animator.SetFloat("MoveAmount", -1.0f);
    }
    public void PlayRun(bool play)
    {
        if (play)
            animator.SetFloat("MoveAmount", 1.0f);
        else
            animator.SetFloat("MoveAmount", -1.0f);
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
    }
    public void ActivateNormalLayer()
    {
        animator.SetLayerWeight(0, 1.0f);
        animator.SetLayerWeight(1, 0.0f);
    }

    //Visuals
    private void UpdateFacing()
    {
        if (InputManager.Instance.InputAxis.x < 0.0f)
            spriteRenderer.flipX = true;
        else if (InputManager.Instance.InputAxis.x > 0.0f)
            spriteRenderer.flipX = false;
    }
}