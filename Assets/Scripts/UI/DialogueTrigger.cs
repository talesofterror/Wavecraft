using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
  public TextAsset jSONDialogue;
  public DialogueFile dialogueFromFile;
  public Dialogue dialogue;

  void Start () {
    if (jSONDialogue) {
      dialogueFromFile = JsonUtility.FromJson<DialogueFile>(jSONDialogue.text);

      for (int i = 0; i < dialogueFromFile.sentences.Length; i++) {
        dialogue.sentences[i] = dialogueFromFile.sentences[i];
      }
    }

    // Debug.Log(dialogueFromFile.sentences[0]);
  }

  public void TriggerDialogue () {
    if (UISingleton.i.cursorTarget.distanceFromTrigger < UISingleton.i.cursorTarget.distanceToInteract){
      GAMESingleton.i.dialogueManager.StartDialogue(dialogue);
      GAMESingleton.i.engaged_Dialogue = true;
    }
  }

  public void EndDialogue() {
    UISingleton.i.ToggleDialogue("off");
    GAMESingleton.i.engaged_Dialogue = false;
  }
}
