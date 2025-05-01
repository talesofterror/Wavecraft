using System.Diagnostics.Tracing;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

  public Dialogue dialogue;

  public void TriggerDialogue () {
    UISingleton.i.ToggleDialogue("on");
  }

  public void EndDialogue() {
    UISingleton.i.ToggleDialogue("off");
  }
}
