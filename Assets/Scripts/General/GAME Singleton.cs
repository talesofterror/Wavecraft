using UnityEngine;

public class GAMESingleton : MonoBehaviour
{
  
private static GAMESingleton _gameSingleton;
public static GAMESingleton i {get {return _gameSingleton;} }

public UISingleton ui;
public DialogueManager dialogueManager;
[HideInInspector] public bool engaged_Dialogue;

  void Awake () {
    if (_gameSingleton != null && _gameSingleton != this) {
      Destroy(this);
    } else {
      _gameSingleton = this;
      DontDestroyOnLoad(this);
    }
  }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (!ui.gameObject.activeSelf) {
      ui.gameObject.SetActive(true);
    }
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
