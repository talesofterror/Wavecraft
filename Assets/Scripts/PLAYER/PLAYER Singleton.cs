using System.Collections;
using UnityEngine;

[SelectionBase]
public class PLAYERSingleton : MonoBehaviour
{

  private static PLAYERSingleton _playerSingleton;
  public static PLAYERSingleton i { get { return _playerSingleton; } }
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
  public bool ignoreDamage = false;

  [Header("Movement")]
  public bool sprintDisabled = false;
  public bool gravityOn = false;
  public float gravityYForce = 10f;

  [Header("State")]
  public bool paused = false;
  [HideInInspector] public bool areaTransition = false;

  void Awake()
  {

    loadedMaterialsArray = new Material[2];

    if (_playerSingleton != null && _playerSingleton != this)
    {
      Destroy(this.gameObject);
    }
    else
    {
      _playerSingleton = this;
      DontDestroyOnLoad(this);
    }
    interactionZ = transform.position.z;
  }

  void Start()
  {
    _renderers = gameObject.GetComponentsInChildren<Renderer>();
    originalMaterialsArray = new Material[_renderers.Length];
    UISingleton.i.HealthValue.text = playerStats.health.ToString();
    LoadMaterials();
  }


  void Update()
  {
  }

  public void PauseToggle(string state)
  {
    if (state == "paused")
    {
      Debug.Log("**Game Paused**");
      paused = true;
      controlsActive = false;
      attackEnabled = false;
      rocket.rigidBody.isKinematic = true;
      Time.timeScale = 0f;
    }
    if (state == "unpaused")
    {
      Debug.Log("**Game Unpaused**");
      paused = false;
      controlsActive = true;
      attackEnabled = true;
      rocket.rigidBody.isKinematic = false;
      Time.timeScale = 1.0f;
    }
  }

  public void takeDamage(float damageAmount, GameObject source = null)
  {
    if (ignoreDamage)
    {
      return;
    }
    else
    {
      if (source == null)
      {
        StartCoroutine(displayDamage(damageDisplayDuration));
        playerStats.health -= damageAmount;
      }
      else
      {
        if (source.CompareTag("Enemy"))
        {
          StartCoroutine(displayDamage(damageDisplayDuration));
          playerStats.health -= damageAmount;
          nudgeDamage(source.transform.position, 10f);
        }
      }
      UISingleton.i.HealthValue.text = playerStats.health.ToString();
    }
  }

  void nudgeDamage(Vector3 source, float force)
  {
    Debug.Log("nudgeDamage called");
    rB.linearVelocity += UTILITY.getDirectionVector3(
        source, transform.position
        ) * force;
  }

  public void collectData()
  {
    playerStats.data++;
    Debug.Log("Player data value: " + playerStats.data);
    UISingleton.i.DataValue.text = playerStats.data.ToString();
  }

  IEnumerator displayDamage(float seconds)
  {
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

  public void ToggleGravity (string state)
  {
    if (state == "on")
    {
      rB.useGravity = true;
    }
  }
}

