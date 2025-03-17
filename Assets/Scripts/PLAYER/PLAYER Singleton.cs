using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;

public class PLAYERSingleton : MonoBehaviour
{

  private static PLAYERSingleton _playerSingleton;
  public static PLAYERSingleton playerSingleton {get {return _playerSingleton;}}

  public Rocket rocket;
  public PLAYERAttack playerAttack;
  public GuyRotate guyRotate;
  [HideInInspector] WORLDInteractable worldCursorTarget;
  public InteractableState_WORLD focusState;
  public bool controlsActive = true;
  public bool attackEnabled = true;
  
  public float interactionZ; 

  void Awake () {
    if (_playerSingleton != null && _playerSingleton != this) {
      Destroy(this.gameObject);
    } else {
      _playerSingleton = this;
      DontDestroyOnLoad(this);
    }
  }

  void Start()
  {
      
  }

  void Update()
  {
    interactionZ = transform.position.z;
  }

  public void PauseToggle (string state) {
    if (state == "paused") {
      controlsActive = false;
      attackEnabled = false;
      rocket.rigidBody.isKinematic = true;
    } if (state == "unpaused") {
      controlsActive = true;
      attackEnabled = true;
      rocket.rigidBody.isKinematic = false;
    }

  }
}
