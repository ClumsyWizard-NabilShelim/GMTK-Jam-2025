using System;
using System.Collections;
using System.Collections.Generic;
using ClumsyWizard.Core;
using TMPro;
using UnityEngine;

public enum CharacterName
{
    Player,
    Girl,
    Snuffles,
    Message_Box
}

public enum DialogueType
{
    Full,
    SpeechBubble,
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

    private WaitForSeconds delay;

    private List<DialogueData> currentDialogues;
    private DialogueType currentDialogueType;
    private int index;
    private bool showingDialogue;
    private bool isTyping;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Speech Dialogue")]
    [SerializeField] private Transform speechBubbleContainer;
    [SerializeField] private TextMeshProUGUI speechkBubbleText;
    [SerializeField] private float autoSkipDelay;

    [Header("Full Dialogue")]
    [SerializeField] private TextMeshProUGUI fullDialogueText;
    private Animator animator;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        animator = GetComponent<Animator>();
        delay = new WaitForSeconds(0.025f);

        InputManager.Instance.OnContinueDialogue += OnContinue;
    }

    public void Show(List<DialogueData> dialogues, Transform target, DialogueType type)
    {
        if (type == DialogueType.Full && IsShowingDialogue)
            return;

        StopAllCoroutines();
        CancelInvoke("OnContinue");

        currentDialogueType = type;

        index = 0;
        currentDialogues = dialogues;
        showingDialogue = true;

        if (type == DialogueType.Full)
        {
            player.Toggle(false);
         
            animator.SetBool("Show", true);
            dialogueText = fullDialogueText;
        }
        else
        {
            dialogueText = speechkBubbleText;
            speechBubbleContainer.gameObject.SetActive(true);
            speechBubbleContainer.position = target.position + new Vector3(1.0f, 1.5f, 0.0f);
            speechBubbleContainer.SetParent(target);
        }

        StartCoroutine(TypeOut(currentDialogues[index]));
    }

    private void Hide()
    {
        if (currentDialogueType == DialogueType.Full)
        {
            player.Toggle(true);
            animator.SetBool("Show", false);
        }
        else
        {
            speechBubbleContainer.gameObject.SetActive(false);
        }

        showingDialogue = false;
    }

    private IEnumerator TypeOut(DialogueData data)
    {
        CancelInvoke("OnContinue");
        isTyping = true;

        if (currentDialogueType == DialogueType.SpeechBubble)
            dialogueText.text = "";
        else
            dialogueText.text = $"{data.Name.ToString().Replace("_", " ")}: ";

        foreach (char letter in data.Dialogue)
        {
            dialogueText.text += letter;
            yield return delay;
        }

        isTyping = false;

        if (currentDialogueType == DialogueType.SpeechBubble)
            Invoke("OnContinue", autoSkipDelay);
    }

    private void OnContinue()
    {
        CancelInvoke("OnContinue");

        if (!showingDialogue)
            return;

        if(isTyping)
        {
            isTyping = false;
            StopAllCoroutines();

            if (currentDialogueType == DialogueType.SpeechBubble)
                dialogueText.text = currentDialogues[index].Dialogue;
            else
                dialogueText.text = $"{currentDialogues[index].Name.ToString().Replace("_", " ")}: ";
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