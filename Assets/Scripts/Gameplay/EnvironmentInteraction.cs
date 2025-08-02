using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueContainer
{
    public List<DialogueData> dialogues;
}

public class EnvironmentInteraction : MonoBehaviour
{
    [SerializeField] private List<DialogueContainer> dialogues;
    [SerializeField] private DialogueType type;

    [Header("Visuals")]
    [SerializeField] private InteractableObjectUI ui;

    private Transform player;

    private void Start()
    {
        InputManager.Instance.OnInteract += OnInteract;
    }


    private void OnInteract()
    {
        if (player == null || dialogues.Count == 0 || DialogueManager.Instance.IsShowingDialogue)
            return;

        DialogueContainer data = dialogues[0];

        if (dialogues.Count > 1 || type == DialogueType.SpeechBubble)
        {
            dialogues.RemoveAt(0);
        }
        
        DialogueManager.Instance.Show(data.dialogues, player, type);

        if(dialogues.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        player = collision.transform;

        if (type == DialogueType.SpeechBubble)
            OnInteract();
        else
            ui.ToggleMarker(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        player = null;
        if (type == DialogueType.Full)
            ui.ToggleMarker(false);
    }

    //Clean up
    private void OnDestroy()
    {
        InputManager.Instance.OnInteract -= OnInteract;
    }
}
