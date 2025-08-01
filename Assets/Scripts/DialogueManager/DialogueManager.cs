using System;
using System.Collections;
using System.Collections.Generic;
using ClumsyWizard.Core;
using TMPro;
using UnityEngine;

public enum CharacterName
{
    Player
}

[Serializable]
public struct DialogueData
{
    public CharacterName Name;
    [TextArea(4, 4)] public string Dialogue;

}

public class DialogueManager : CW_Singleton<DialogueManager>
{
    private Player player;

    public bool IsShowingDialogue => showingDialogue;

    private Animator animator;
    private List<DialogueData> currentDialogues;
    private int index;
    private WaitForSeconds delay;
    private bool showingDialogue;
    private bool isTyping;

    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        animator = GetComponent<Animator>();
        delay = new WaitForSeconds(0.025f);

        InputManager.Instance.OnContinueDialogue += OnContinue;
    }

    public void Show(List<DialogueData> dialogues)
    {
        player.Toggle(false);
        index = 0;
        currentDialogues = dialogues;
        showingDialogue = true;
        animator.SetBool("Show", true);
        StartCoroutine(TypeOut(currentDialogues[index]));
    }

    private void Hide()
    {
        player.Toggle(true);
        animator.SetBool("Show", false);
        showingDialogue = false;
    }

    private IEnumerator TypeOut(DialogueData data)
    {
        isTyping = true;
        dialogueText.text = $"{data.Name}: ";
        yield return new WaitForSeconds(0.5f);

        foreach (char letter in data.Dialogue)
        {
            dialogueText.text += letter;
            yield return delay;
        }

        isTyping = false;
    }

    private void OnContinue()
    {
        if (!showingDialogue)
            return;

        if(isTyping)
        {
            isTyping = false;
            StopAllCoroutines();

            dialogueText.text = $"{currentDialogues[index].Name}: {currentDialogues[index].Dialogue}";
        }
        else
        {
            index++;

            if (index >= currentDialogues.Count)
                Hide();
            else
                StartCoroutine(TypeOut(currentDialogues[index]));
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnContinueDialogue -= OnContinue;
    }
}