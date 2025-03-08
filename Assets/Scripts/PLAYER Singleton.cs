using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;

public class PLAYERSingleton : MonoBehaviour
{

  private static PLAYERSingleton _playerSingleton;
  public static PLAYERSingleton playerSingleton {get {return _playerSingleton;}}

  public Rocket rocket;
  public PLAYERAttack playerAttck;
  public GuyRotate guyRotate;
  public WORLDInteractable worldCursorTarget;
  
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
}
