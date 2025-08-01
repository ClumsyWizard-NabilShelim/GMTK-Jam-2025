using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider2D blockingCollider;
    [SerializeField] private bool isOpen;

    [Header("Visuals")]
    [SerializeField] private InteractableObjectUI ui;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    private bool playerNear;

    private void Start()
    {
        InputManager.Instance.OnInteract += OnInteract;

        Toggle(isOpen);
    }

    private void Toggle(bool active)
    {
        isOpen = active;

        blockingCollider.enabled = !isOpen;

        if (isOpen)
            spriteRenderer.sprite = openSprite;
        else
            spriteRenderer.sprite = closeSprite;
    }

    private void OnInteract()
    {
        if (!playerNear)
            return;

        Toggle(!isOpen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerNear = true;
        ui.ToggleMarker(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerNear = false;
        ui.ToggleMarker(false);
    }

    //Clean up
    private void OnDestroy()
    {
        InputManager.Instance.OnInteract -= OnInteract;
    }
}
