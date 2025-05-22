using UnityEditor;
using UnityEngine;

public class ProgressTokenCase : MonoBehaviour
{

  public ProgressToken[] tokens;
  [HideInInspector] public int dialogueIndexState;
  [HideInInspector] public ProgressToken activeToken;
  [HideInInspector] public bool tokensDepleted { get; private set; }

  void Awake()
  {
    activeToken = tokens[dialogueIndexState];
    activeToken.activated = true;
    activeToken.setTokenType();
  }

  public bool evaluateToken(ProgressToken token)
  {
    return token.getTokenStatus();
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
    if (activeToken == tokens[tokens.Length - 1])
    {
      tokensDepleted = true;
    }
    else
    {
      activeToken = tokens[dialogueIndexState];
    }
    activeToken.activated = true;
    activeToken.setTokenType();
    Debug.Log(transform.name + " called advanceDialogue(). dialogueIndexState = " + dialogueIndexState);
  }

}
