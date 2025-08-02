using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider2D blockingCollider;
    [SerializeField] private bool isOpen;
    [SerializeField] private string requiredKey;
    [SerializeField] private List<DialogueData> lockedDoorDialogues;

    public Action<bool> OnDoorInteract;

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

        if (blockingCollider != null)
            blockingCollider.enabled = !isOpen;

        if (isOpen)
            spriteRenderer.sprite = openSprite;
        else
            spriteRenderer.sprite = closeSprite;

        OnDoorInteract?.Invoke(isOpen);
    }

    private void OnInteract()
    {
        if (!playerNear)
            return;

        if (requiredKey != "" && !PlayerInventory.Instance.HasItem(requiredKey))
        {
            if(lockedDoorDialogues != null && lockedDoorDialogues.Count > 0)
                DialogueManager.Instance.Show(lockedDoorDialogues, transform, DialogueType.SpeechBubble);
            return;
        }

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
