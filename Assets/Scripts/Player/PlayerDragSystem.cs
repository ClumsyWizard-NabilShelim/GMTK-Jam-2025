using UnityEngine;

public class PlayerDragSystem : MonoBehaviour
{
    private Player player;
    private Rigidbody2D dragRB;
    private float defaultGravity;

    public void Initialize(Player player)
    {
        this.player = player;
    }

    private void FixedUpdate()
    {
        if (dragRB == null)
            return;

        dragRB.linearVelocity = player.RB.linearVelocity;
    }

    public void AttachObject(Rigidbody2D rb)
    {
        if(dragRB != null)
            Detach();

        dragRB = rb;
        defaultGravity = rb.gravityScale;
        rb.gravityScale = 0.0f;
        dragRB.constraints = RigidbodyConstraints2D.None;
        dragRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        player.StateModifier.SetState(PlayerModifiedState.Dragging);
    }

    public void Detach()
    {
        if (dragRB == null)
            return;

        dragRB.gravityScale = defaultGravity;
        dragRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        dragRB.constraints = RigidbodyConstraints2D.FreezePositionX;

        player.StateModifier.SetState(PlayerModifiedState.None);
        dragRB = null;
    }
}