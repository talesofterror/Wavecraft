using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;

[SelectionBase]
public class PLAYERSingleton : MonoBehaviour
{

  private static PLAYERSingleton _playerSingleton;
  public static PLAYERSingleton i {get {return _playerSingleton;}}
  [Header("Components")]
  public Rocket rocket;
  public PlayerStats playerStats;
  public PlayerControls playerControls;
  public PlayerAttack playerAttack;
  public GuyRotate guyRotate;
  public Rigidbody rB;
  
  [HideInInspector] Interactable worldCursorTarget;
  [HideInInspector] public InteractableState_WORLD focusState;
  [HideInInspector] public bool controlsActive = true;
  [HideInInspector] public bool attackEnabled = true;
  [HideInInspector] public float interactionZ; 
  
  [Header("Materials")]
  public Material damageIndicatorMaterial;
  Material[] loadedMaterialsArray;
  Material[] originalMaterialsArray;
  Renderer[] _renderers;
  [Header("Taking Damage")]
  public float damageDisplayDuration = 1;

  void Awake () {

    loadedMaterialsArray = new Material[2];

    if (_playerSingleton != null && _playerSingleton != this) {
      Destroy(this.gameObject);
    } else {
      _playerSingleton = this;
      DontDestroyOnLoad(this);
    }
  }

  void Start()
  {
    _renderers = gameObject.GetComponentsInChildren<Renderer>();
    originalMaterialsArray = new Material[_renderers.Length];
    interactionZ = transform.position.z;
    UISingleton.i.HealthValue.text = playerStats.health.ToString();
    LoadMaterials();
  }


  void Update()
  {
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

  public void takeDamage(float damageAmount) {
    StartCoroutine(displayDamage(damageDisplayDuration));
    playerStats.health -= damageAmount;
    UISingleton.i.HealthValue.text = playerStats.health.ToString();
  }

  IEnumerator displayDamage(float seconds) {
    for (int i = 0; i < _renderers.Length; i++)
    {
      _renderers[i].material = loadedMaterialsArray[0];
    }
    yield return new WaitForSeconds(seconds);
    for (int i = 0; i < _renderers.Length; i++)
    {
      _renderers[i].material = originalMaterialsArray[i];
    }
    yield break;
  }
  
  private void LoadMaterials()
  {
    for (int i = 0; i < _renderers.Length; i++)
    {
      originalMaterialsArray[i] = _renderers[i].material;
    }
    loadedMaterialsArray[0] = damageIndicatorMaterial;
  }
}

