using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    [SerializeField] private InteractableObjectUI interactableObjectUI;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InputManager.Instance.OnDragDrop += OnDragDrop;
    }

    //Events
    private void OnDragDrop()
    {
        if (player == null)
            return;

        if ((player.State == PlayerState.Idle || player.State == PlayerState.Move) && player.StateModifier.State != PlayerModifiedState.Climbing)
        {
            if(player.StateModifier.State == PlayerModifiedState.Dragging)
                player.DragSystem.Detach();
            else
                player.DragSystem.AttachObject(rb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        player = collision.GetComponent<Player>();
        interactableObjectUI.ToggleMarker(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (player != null)
        {
            player.DragSystem.Detach();
            player.StateModifier.SetState(PlayerModifiedState.None);
        }

        player = null;
        interactableObjectUI.ToggleMarker(false);
    }

    //Clean Up
    private void OnDestroy()
    {
        InputManager.Instance.OnDragDrop -= OnDragDrop;
    }
}