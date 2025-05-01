using UnityEngine;
using TMPro;

[SelectionBase]

public class WORLDInteractable : MonoBehaviour
{
  public string _name;
  [HideInInspector] public string _tag;
  [HideInInspector] public Vector3 position;
  [HideInInspector] public InteractableState_WORLD state = InteractableState_WORLD.NotEngaged;
  [HideInInspector] public bool isInRange;
  [HideInInspector] public float distanceFromTrigger;
  [SerializeField] public float distanceToInteract;

  public enum Dispo {
    Friendly,
    Hostile,
    Neutral
  }

  public Dispo dispo = new Dispo();

  public Color displayNameColor;
  

  void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, distanceToInteract);
  }

  void Awake () {
    // interactableObject.transform.localPosition = interactableObject.transform.localPosition
    //   + new Vector3(0, 0, PLAYERSingleton.playerSingleton.interactionZ);
  }

  void Start()
  {
      _tag = gameObject.tag;
      position = gameObject.transform.position;
      state = InteractableState_WORLD.NotEngaged;
  }
  
  void Update()
  {
    if (state == InteractableState_WORLD.Hover) {
      distanceFromTrigger = calculateDistance();
      ClickBehavior(); 
    }
    if (state == InteractableState_WORLD.Focus) {
      if (Input.GetKeyDown(KeyCode.Escape)) {

      }
    }
  }
  
  float calculateDistance () {
    float d = Vector3.Distance(transform.position, PLAYERSingleton.i.transform.position);
    return d;
  }

  void OnTriggerEnter(Collider collider) {
    if (state == InteractableState_WORLD.NotEngaged)
    {
      HoverBehavior(collider);
    }
  }

  public void HoverBehavior(Collider collider)
  {
    if (collider.CompareTag("Pointer"))
    {
      state = InteractableState_WORLD.Hover;
      UISingleton.i.cursorTarget = this;
      UISingleton.i._cursor.UpdateAppearance();
      UISingleton.i.ToggleIdentifierPanel("on");
      UISingleton.i.IdentifierText.text = _name;
      // if (dispo == Dispo.Hostile) {
      //   UISingleton.i.IdentifierText.color = Color.red;
      // }
      Debug.Log("Interactable: " + _name + " (" + transform.name + ").");
    }

  }

  private void ClickBehavior () {
    if (PLAYERSingleton.i.playerControls.interactAction.WasPressedThisFrame() && distanceFromTrigger < distanceToInteract) {
      if (dispo == Dispo.Friendly) {
        print("Interactable ClickBehavior() called");
        state = InteractableState_WORLD.Focus;
        UISingleton.i.ToggleDialogue("on");
      }
      if (dispo == Dispo.Hostile) {
        
      }
      if (dispo == Dispo.Hostile) {
        
      }
    }
  }

  void OnTriggerExit ()
  {
    NotEngagedBehavior();
  }

  public void NotEngagedBehavior()
  {
    state = InteractableState_WORLD.NotEngaged;
    UISingleton.i.ToggleIdentifierPanel("off");
    UISingleton.i.ToggleDialogue("off");
  }
}

public enum InteractableState_WORLD {
  Hover, 
  Focus,
  NotEngaged
}
