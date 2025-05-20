using UnityEngine;


// * FILE /////////////////////
[System.Serializable]
public class DialogueFile
{
  public DialogueSet[] dialogueSets;
  public string[] defaultDialogue;
  public int dataThreshhold;

  public string[] LoadDefaultDialogue()
  {
    return this.defaultDialogue;
  }

  public DialogueSet[] LoadDialogueSets()
  {
    return this.dialogueSets;
  }
}

// * SETS /////////////////////
[System.Serializable]
public class DialogueSet
{
  public string keyword;
  public string[] dialogue;

  public string[] LoadDialogue(Dialogue dialogue) {
    return this.dialogue;
  }
}

// [SerializeField]
// public struct DialogueState
// {
// }
