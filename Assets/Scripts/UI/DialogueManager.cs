using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
  public Queue<string> sentences;

  void Start()
  {
      sentences = new Queue<string>();
  }

  void Update()
  {
      
  }

  public void StartDialogue (Dialogue dialogue) {

    foreach (string sentence in dialogue.sentenceArray) {
      sentences.Enqueue(sentence);
    }

    UISingleton.i.ToggleDialogue("on");

    DisplayNextSentence();
  }

  public void DisplayNextSentence () {
    if (sentences.Count == 0) {
      EndDialogue();
      return;
    }

    string sentence = sentences.Dequeue();
    UISingleton.i.NameText.text = UISingleton.i.cursorTarget._name;
    UISingleton.i.DialogueText.text = sentence;

    Debug.Log(sentence);
  }

  public void EndDialogue () {
    sentences.Clear();
    UISingleton.i.ToggleDialogue("off");
    GAMESingleton.i.engaged_Dialogue = false;
  }
}
