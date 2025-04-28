using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;

[SelectionBase]
public class PLAYERSingleton : MonoBehaviour
{

  private static PLAYERSingleton _playerSingleton;
  public static PLAYERSingleton playerSingleton {get {return _playerSingleton;}}

  public Rocket rocket;
  public PLAYERAttack playerAttack;
  public GuyRotate guyRotate;
  public Rigidbody rB;
  [HideInInspector] WORLDInteractable worldCursorTarget;
  public InteractableState_WORLD focusState;
  public bool controlsActive = true;
  public bool attackEnabled = true;
  
  [Header("Materials")]
  public Material damageIndicatorMaterial;

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
    Renderer[] _renderers = gameObject.GetComponentsInChildren<Renderer>();

    for (int i = 0; i < _renderers.Length; i++) {
      Material[] _newMaterials = new Material[2];
      _newMaterials[0] = _renderers[i].material;
      _newMaterials[1] = damageIndicatorMaterial;
      _renderers[i].materials = _newMaterials;
    }
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
