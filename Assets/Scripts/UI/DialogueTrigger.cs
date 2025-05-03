using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
  public TextAsset jSONDialogue;
  public DialogueFile dialogueFromFile;
  public Dialogue dialogue;

  void Awake()
  {
    if (jSONDialogue) {
      dialogueFromFile = JsonUtility.FromJson<DialogueFile>(jSONDialogue.text);
      dialogue.sentences = new string[dialogueFromFile.sentences.Length];

      for (int i = 0; i < dialogueFromFile.sentences.Length; i++) {
        dialogue.sentences[i] = dialogueFromFile.sentences[i];
      }
    }
  }

  void Start () {
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
