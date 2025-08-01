using System;
using System.Collections;
using UnityEngine;

public enum PlayerModifiedState
{
    None,
    Running,
    Crouching,
    Dragging,
    Climbing
}

public class PlayerStateModifier : MonoBehaviour
{
    private Player player;
    public PlayerModifiedState State;
    public Action OnStatModified;

    public void Initialize(Player player)
    {
        this.player = player;
    }

    public void SetState(PlayerModifiedState state)
    {
        State = state;
        OnStatModified?.Invoke();

        switch (State)
        {
            case PlayerModifiedState.None:
                player.Visuals.ActivateNormalLayer();
                break;
            case PlayerModifiedState.Running:
                player.Visuals.ActivateNormalLayer();
                break;
            case PlayerModifiedState.Crouching:
                player.Visuals.ActivateCrouchLayer();
                break;
            case PlayerModifiedState.Dragging:
                player.Visuals.ActivateDragLayer();
                break;
            case PlayerModifiedState.Climbing:
                break;
            default:
                break;
        }
    }

    public bool IsInLockedState()
    {
        return State == PlayerModifiedState.Climbing || State == PlayerModifiedState.Dragging;
    }
}