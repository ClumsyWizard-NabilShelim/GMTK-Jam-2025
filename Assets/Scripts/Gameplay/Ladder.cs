using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player player;

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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (player != null)
            player.StateModifier.SetState(PlayerModifiedState.None);

        player = null;
    }
}