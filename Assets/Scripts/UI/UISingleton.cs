using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISingleton : MonoBehaviour
{

private static UISingleton _uiSingleton;
public static UISingleton uiSingleton {get {return _uiSingleton;} }

[HideInInspector] public UICursor _cursor;
[HideInInspector] public WORLDInteractable cursorTarget;

public GameObject DataHeader;
public TextMeshProUGUI DataValue;
public GameObject DialogueContainer;
public GameObject ViewportPanel;
public TextMeshProUGUI NameText;
public TextMeshProUGUI DialogueText;
public GameObject IdentifierContainer;
public TextMeshProUGUI IdentifierText;

  void Awake () {
    if (_uiSingleton != null && _uiSingleton != this) {
      Destroy(this);
    } else {
      _uiSingleton = this;
      DontDestroyOnLoad(this);
    }
  }

  void Start () {
    _cursor = CAMERASingleton.cameraSingleton.uICursor;
    ToggleDialogue("off");
  }

  public void ToggleDialogue (String state) {
    if (state == "off") {
      DialogueContainer.SetActive(false);
      PLAYERSingleton.playerSingleton.PauseToggle("unpaused");
    } else if (state == "on") {
      DialogueContainer.SetActive(true);
      PLAYERSingleton.playerSingleton.PauseToggle("paused");
    }
  }

  public void ToggleIdentifierPanel (String state) {
    if (state == "off") {
      IdentifierContainer.SetActive(false);
    } else if (state == "on") {
      IdentifierContainer.SetActive(true);  
    }
  }

}
