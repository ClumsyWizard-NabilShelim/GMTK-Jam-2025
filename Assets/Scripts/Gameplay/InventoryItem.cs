using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private List<DialogueData> dialogues;
    [SerializeField] private string itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        DialogueManager.Instance.Show(dialogues, collision.transform, DialogueType.SpeechBubble);
        PlayerInventory.Instance.AddItem(itemType);
        Destroy(gameObject);
    }
}