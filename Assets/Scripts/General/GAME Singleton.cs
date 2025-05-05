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

    if (!ui.gameObject.activeInHierarchy) {
      ui.gameObject.SetActive(true);
    }
  }

  void Start()
  {
  }

  void Update()
  {
      
  }
}
