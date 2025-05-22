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
  public int dataThreshhold;

  public ProgressTokenCase tokenCase;

  void Awake()
  {
    if (jSONDialogue)
    {
      dialogueFile = JsonUtility.FromJson<DialogueFile>(jSONDialogue.text);
      dialogueSets = dialogueFile.LoadDialogueSets();
      dataThreshhold = dialogueFile.dataThreshhold;
    }

    try
    {
      tokenCase = GetComponent<ProgressTokenCase>();
    }
    catch (Exception e)
    {
      return;
    }

  }

  public void TriggerDialogue () {
    if (PLAYERSingleton.i.playerStats.data >= dataThreshhold)
    {
      if (!tokenCase.activeToken.complete && tokenCase.evaluateToken(tokenCase.activeToken))
      {
        tokenCase.advanceDialogue();
      }
      Debug.Log("Active Token name: " + tokenCase.activeToken.name);
      Debug.Log("Active Token reference object: " + tokenCase.activeToken.referenceObject);
      activeDialogue.sentenceArray = dialogueSets[tokenCase.dialogueIndexState].LoadDialogue(activeDialogue);
      GAMESingleton.i.dialogueManager.StartDialogue(activeDialogue);
      GAMESingleton.i.engaged_Dialogue = true;
    }
    else
    {
      activeDialogue.sentenceArray = dialogueFile.LoadDefaultDialogue();
      GAMESingleton.i.dialogueManager.StartDialogue(activeDialogue);
      GAMESingleton.i.engaged_Dialogue = true;
    }
  }

  public void EndDialogue() {
    GAMESingleton.i.dialogueManager.EndDialogue();
  }
}
