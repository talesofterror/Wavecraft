using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
  public TextAsset jSONDialogue;
  public DialogueFile dialogueFile;
  public Dialogue dialogue;

  void Awake()
  {
    if (jSONDialogue) {
      dialogueFile = JsonUtility.FromJson<DialogueFile>(jSONDialogue.text);
      dialogue.sentences = new string[dialogueFile.sentences.Length];

      for (int i = 0; i < dialogueFile.sentences.Length; i++) {
        dialogue.sentences[i] = dialogueFile.sentences[i];
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
    GAMESingleton.i.dialogueManager.EndDialogue();
    // UISingleton.i.ToggleDialogue("off"); 
    // GAMESingleton.i.engaged_Dialogue = false;
  }
}
