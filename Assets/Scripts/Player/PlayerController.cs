using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    private bool isRunning;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        InputManager.Instance.OnRunStart += OnRunStart;
        InputManager.Instance.OnRunEnd += OnRunEnd;
    }

    private void Update()
    {
        if (Mathf.Abs(rb.linearVelocityX) != 0.0f)
            animator.SetFloat("MoveAmount", isRunning ? 1.0f : 0.5f);
        else
            animator.SetFloat("MoveAmount", -1.0f);

        UpdateFacing();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = InputManager.Instance.InputAxis.x * (isRunning ? runSpeed : moveSpeed);
    }

    //Visuals
    private void UpdateFacing()
    {
        spriteRenderer.flipX = InputManager.Instance.InputAxis.x < 0.0f;
    }

    //Input Events
    private void OnRunStart()
    {
        isRunning = true;
    }
    private void OnRunEnd()
    {
        isRunning = false;
    }

    //Clean up
    private void OnDestroy()
    {
        InputManager.Instance.OnRunStart -= OnRunStart;
        InputManager.Instance.OnRunEnd -= OnRunEnd;
    }
}