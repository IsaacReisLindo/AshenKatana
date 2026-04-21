
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class NPC : MonoBehaviour, IInteractable {
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Image portraitImage;
    public TMP_Text nameText;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract() {
        return !isDialogueActive;
    }

    public void Interact() {
        if (dialogueData == null || (PauseControler.IsGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive) {
            //NextLine


        } else {
            StartDialogue();
        }
    }
    void StartDialogue() {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.name);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        PauseControler.SetPause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine() {
        if (isTyping) {
            //Skip Typing animation and show the full line
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialoguesLines[dialogueIndex]);
            isTyping = false;

            //If another line, type next line
        } else if (++dialogueIndex + 1 < dialogueData.dialoguesLines.Length) {
            StartCoroutine(TypeLine());
        } else {

            EndDialogue();
        }
    }

    IEnumerator TypeLine() {

        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialoguesLines[dialogueIndex]) {

            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);

            //DisplayNextLine
        }
    }
    public void EndDialogue() {

        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false); 
        PauseControler.SetPause(false);
    }
}

