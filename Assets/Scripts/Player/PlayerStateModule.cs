using System.Collections;
using UnityEngine;

public abstract class PlayerStateModule : MonoBehaviour
{
    protected Player player;
    public abstract PlayerState TargetState { get; }

    public virtual void Initialize(Player player)
    {
        this.player = player;
    }

    public abstract void Enter(PlayerState previousState);
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void Exit(PlayerState nextState);
}