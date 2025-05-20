using UnityEditor;
using UnityEngine;

public class ProgressTokenCase : MonoBehaviour
{

  public ProgressToken[] tokens;
  [HideInInspector] public int dialogueIndexState;
  [HideInInspector] public ProgressToken activeToken;

  void Awake()
  {
    activeToken = tokens[dialogueIndexState];
    activeToken.activated = true;
  }

  public void evaluateToken(ProgressToken token)
  {
    token.getTokenStatus();
  }

  public bool areAnyTokensActive()
  {
    int i;
    for (i = 0; i < tokens.Length; i++)
    {
      if (tokens[i].activated) { return true; }
    }
    if (i == tokens.Length - 1) { return false; }
    return false;
  }

  public void advanceDialogue()
  {
    activeToken.activated = false;
    activeToken.complete = true;
    dialogueIndexState++;
    activeToken = tokens[dialogueIndexState];
  }

}
