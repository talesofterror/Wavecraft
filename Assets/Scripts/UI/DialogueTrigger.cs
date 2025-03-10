using System.Diagnostics.Tracing;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

  public Dialogue dialogue;

  public void TriggerDialogue () {
    UISingleton.uiSingleton.ToggleDialogue("on");
  }

  public void EndDialogue() {
    UISingleton.uiSingleton.ToggleDialogue("off");
  }
}
