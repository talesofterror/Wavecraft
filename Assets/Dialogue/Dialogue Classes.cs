using UnityEngine;


// * FILE /////////////////////
[System.Serializable]
public class DialogueFile
{
  public DialogueSet[] dialogueSets;
  public string[] defaultDialogue;
  public int dataThreshhold;

  public string[] loadDefaultDialogue()
  {
    return this.defaultDialogue;
  }
}

// * SETS /////////////////////
[System.Serializable]
public class DialogueSet
{
  public string keyword;
  public string[] dialogue;

  public string[] loadDialogue(Dialogue dialogue) {
    return this.dialogue;
  }
}

// [SerializeField]
// public struct DialogueState
// {
// }
