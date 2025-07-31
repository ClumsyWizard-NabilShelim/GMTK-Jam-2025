using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Init = -1,
    Idle,
    Walk,
    Run,
    Vault
}

public class Player : MonoBehaviour
{
    public PlayerState State { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsCrouching { get; private set; }

    public Rigidbody2D RB { get; private set; }
    public PlayerEdgeDetector EdgeDetector { get; private set; }
    public PlayerVisuals Visuals { get; private set; }
    private Dictionary<PlayerState, PlayerStateModule> modules;

    private float defaultGravity;
    
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        defaultGravity = RB.gravityScale;

        EdgeDetector = GetComponent<PlayerEdgeDetector>();
        Visuals = GetComponent<PlayerVisuals>();

        InputManager.Instance.OnRunStart += OnRunStart;
        InputManager.Instance.OnRunEnd += OnRunEnd;
        InputManager.Instance.OnCrouchToggle += OnCrouchToggle;
        InputManager.Instance.OnVault += OnVault;

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
        if(EdgeDetector.IsNearLedge())
        {
            //Interaction Manager will show vault button prompt
        }

        modules[State].UpdateState();
    }

    private void FixedUpdate()
    {
        modules[State].FixedUpdateState();
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
        if (State == PlayerState.Vault)
            return;

        IsRunning = true;
        IsCrouching = false;
        UpdateCrouchState();

        if (InputManager.Instance.InputAxis.x != 0.0f)
            SetState(PlayerState.Run);
    }
    private void OnRunEnd()
    {
        if (State == PlayerState.Vault)
            return;

        IsRunning = false;

        if (InputManager.Instance.InputAxis.x != 0.0f)
            SetState(PlayerState.Walk);
    }
    private void OnCrouchToggle()
    {
        if (State == PlayerState.Vault)
            return;

        IsCrouching = !IsCrouching;
        IsRunning = false;

        UpdateCrouchState();
    }

    private void OnVault()
    {
        if (State == PlayerState.Vault)
            return;

        if (EdgeDetector.IsNearLedge())
            SetState(PlayerState.Vault);
    }

    //Helper Function
    private void UpdateCrouchState()
    {
        if (IsCrouching)
            Visuals.ActivateCrouchLayer();
        else
            Visuals.ActivateNormalLayer();

        SetState(PlayerState.Idle);
    }

    public void ToggleGravity(bool toggle)
    {
        if (toggle)
            RB.gravityScale = defaultGravity;
        else
            RB.gravityScale = 0.0f;

        RB.linearVelocity = Vector2.zero;
    }

    //Clean up
    private void OnDestroy()
    {
        InputManager.Instance.OnRunStart -= OnRunStart;
        InputManager.Instance.OnRunEnd -= OnRunEnd;
        InputManager.Instance.OnCrouchToggle -= OnCrouchToggle;
    }
}