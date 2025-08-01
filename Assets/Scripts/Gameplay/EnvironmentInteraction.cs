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

    [Header("Visuals")]
    [SerializeField] private InteractableObjectUI ui;

    private bool playerNear;

    private void Start()
    {
        InputManager.Instance.OnInteract += OnInteract;
    }


    private void OnInteract()
    {
        if (!playerNear || dialogues.Count == 0 || DialogueManager.Instance.IsShowingDialogue)
            return;

        DialogueContainer data = dialogues[0];

        if (dialogues.Count > 1)
            dialogues.RemoveAt(0);

        DialogueManager.Instance.Show(data.dialogues);
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
