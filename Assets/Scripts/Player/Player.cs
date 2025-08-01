using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum PlayerState
{
    Init = -1,
    Idle,
    Move,
    Vault,
    Combat,
    Jump
}

public class Player : MonoBehaviour
{
    public PlayerState State { get; private set; }
    public Vector2 Facing { get; private set; }

    public Rigidbody2D RB { get; private set; }
    public PlayerEdgeDetector EdgeDetector { get; private set; }
    public PlayerDragSystem DragSystem { get; private set; }
    public PlayerVisuals Visuals { get; private set; }
    public PlayerStateModifier StateModifier { get; private set; }
    private Dictionary<PlayerState, PlayerStateModule> modules;

    private float defaultGravity;

    [Header("Ground Detection")]
    [SerializeField] private Vector2 checkArea;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        defaultGravity = RB.gravityScale;

        EdgeDetector = GetComponent<PlayerEdgeDetector>();
        Visuals = GetComponent<PlayerVisuals>();

        DragSystem = GetComponent<PlayerDragSystem>();
        DragSystem.Initialize(this);

        StateModifier = GetComponent<PlayerStateModifier>();
        StateModifier.Initialize(this);

        Facing = Vector2.right;

        InputManager.Instance.OnRunStart += OnRunStart;
        InputManager.Instance.OnRunEnd += OnRunEnd;
        InputManager.Instance.OnCrouchToggle += OnCrouchToggle;
        InputManager.Instance.OnAttack += OnAttack;
        InputManager.Instance.OnJump += OnJump;

        modules = new Dictionary<PlayerState, PlayerStateModule>();
        foreach (PlayerStateModule module in GetComponents<PlayerStateModule>())
        {
            module.Initialize(this);
            modules.Add(module.TargetState, module);
        }

        State = PlayerState.Init;
        SetState(PlayerState.Idle);
    }

    private void Update()
    {
        LedgeDetection();

        if (!StateModifier.IsInLockedState())
        {
            if (InputManager.Instance.InputAxis.x > 0.0f)
                Facing = Vector2.right;
            else if (InputManager.Instance.InputAxis.x < 0.0f)
                Facing = Vector2.left;
        }

        modules[State].UpdateState();
    }

    private void FixedUpdate()
    {
        modules[State].FixedUpdateState();
    }

    //Actions
    private void LedgeDetection()
    {
        if (State == PlayerState.Vault || IsGrounded())
            return;

        if (EdgeDetector.IsNearLedge())
            SetState(PlayerState.Vault);
    }

    //Event
    public void SetState(PlayerState state)
    {
        if (State == state)
            return;

        PlayerState prev = State;

        if(modules.ContainsKey(prev))
            modules[prev].Exit(state);

        State = state;

        if (modules.ContainsKey(State))
            modules[State].Enter(prev);
    }

    //Input Events
    private void OnRunStart()
    {
        if (State == PlayerState.Vault || StateModifier.IsInLockedState())
            return;

        if (InputManager.Instance.InputAxis.x != 0.0f)
            StateModifier.SetState(PlayerModifiedState.Running);
    }
    private void OnRunEnd()
    {
        if (State == PlayerState.Vault || StateModifier.State != PlayerModifiedState.Running || StateModifier.IsInLockedState())
            return;

        StateModifier.SetState(PlayerModifiedState.None);
    }
    private void OnCrouchToggle()
    {
        if (State == PlayerState.Vault || StateModifier.IsInLockedState())
            return;

        if (StateModifier.State == PlayerModifiedState.Crouching)
            StateModifier.SetState(PlayerModifiedState.None);
        else
            StateModifier.SetState(PlayerModifiedState.Crouching);
    }

    private void OnAttack()
    {
        if (State == PlayerState.Vault || State == PlayerState.Combat || StateModifier.IsInLockedState())
            return;

        SetState(PlayerState.Combat);
    }

    private void OnJump()
    {
        if(State == PlayerState.Vault || State == PlayerState.Jump || StateModifier.State == PlayerModifiedState.Dragging)
            return;

        StateModifier.SetState(PlayerModifiedState.None);
        SetState(PlayerState.Jump);
    }

    //Helper Function
    public void ToggleGravity(bool toggle)
    {
        if (toggle)
            RB.gravityScale = defaultGravity;
        else
            RB.gravityScale = 0.0f;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position, checkArea, 0.0f, groundLayer) != null;
    }

    //Clean up
    private void OnDestroy()
    {
        InputManager.Instance.OnRunStart -= OnRunStart;
        InputManager.Instance.OnRunEnd -= OnRunEnd;
        InputManager.Instance.OnCrouchToggle -= OnCrouchToggle;
        InputManager.Instance.OnAttack -= OnAttack;
        InputManager.Instance.OnJump -= OnJump;
    }

    //Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, checkArea);
    }
}