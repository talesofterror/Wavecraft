using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
  public Queue<string> sentences;

  void Start()
  {
      
  }

  void Update()
  {
      
  }

  public void StartDialogue (Dialogue dialogue) {

    sentences.Clear();

    foreach (string sentence in dialogue.sentences) {
      sentences.Enqueue(sentence);
    }

    
  }

  public void DisplayNextSentence () {
    
  }
}
