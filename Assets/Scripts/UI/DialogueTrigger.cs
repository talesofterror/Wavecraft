using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
  public TextAsset jSONDialogue;
  public DialogueFile dialogueFile;
  public Dialogue activeDialogue;
  public string[] defaultDialogue;
  public DialogueSet[] dialogueSets;

  // ! JSON with comments..... 

  void Awake()
  {
    if (jSONDialogue)
    {
      dialogueFile = JsonUtility.FromJson<DialogueFile>(jSONDialogue.text);
      dialogueSets = new DialogueSet[dialogueFile.dialogueSets.Length];
      for (int i = 0; i < dialogueFile.dialogueSets.Length; i++)
      {
        dialogueSets[i] = dialogueFile.dialogueSets[i];
      }
    }
  }

  void Start () {
    // Debug.Log(dialogueFromFile.sentences[0]);
  }

  public void TriggerDialogue () {
    if (PLAYERSingleton.i.playerStats.data >= dialogueFile.dataThreshhold)
    {
      activeDialogue.sentenceArray = dialogueSets[0].loadDialogue(activeDialogue);
      GAMESingleton.i.dialogueManager.StartDialogue(activeDialogue);
      GAMESingleton.i.engaged_Dialogue = true;
    }
    else
    {
      activeDialogue.sentenceArray = dialogueFile.loadDefaultDialogue();
      for (int i = 0; i < dialogueFile.defaultDialogue.Length; i++)
      {
        activeDialogue.sentenceArray[i] = dialogueFile.defaultDialogue[i];
      }
      GAMESingleton.i.dialogueManager.StartDialogue(activeDialogue);
      GAMESingleton.i.engaged_Dialogue = true;
    }
  }

  public void EndDialogue() {
    GAMESingleton.i.dialogueManager.EndDialogue();
    // UISingleton.i.ToggleDialogue("off"); 
    // GAMESingleton.i.engaged_Dialogue = false;
  }
}
