using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class WORLDInteractable : MonoBehaviour
{
  string tag;
  string name;
  Vector3 position;
  InteractableState_WORLD state;

    void Awake () {
      GetComponent<BoxCollider>().center += new Vector3(0, 0, PLAYERSingleton.playerSingleton.interactionZ);
    }

    void Start()
    {
        name = gameObject.name;
        tag = gameObject.tag;
        position = gameObject.transform.position;
    }

    void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Pointer")) {
        print("Interactable: " + name + ".");
        print("Player singleton Z = " + PLAYERSingleton.playerSingleton.interactionZ);
      }
    }

    void Update()
    {
        
    }
}

enum InteractableState_WORLD {
  Hover, 
  Engaged,
  NotEngaged
}
