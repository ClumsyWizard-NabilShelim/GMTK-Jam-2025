using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player player;
    [SerializeField] private InteractableObjectUI interactUI;

    private void Update()
    {
        if (player == null)
            return;

        if(InputManager.Instance.InputAxis.y != 0.0f && player.State == PlayerState.Idle)
        {
            player.StateModifier.SetState(PlayerModifiedState.Climbing);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        player = collision.GetComponent<Player>();

        interactUI.transform.position = new Vector3(transform.position.x, player.transform.position.y + 1.5f, 0.0f);
        interactUI.ToggleMarker(true);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (player != null)
            player.StateModifier.SetState(PlayerModifiedState.None);

        player = null;
        interactUI.ToggleMarker(false);
    }
}