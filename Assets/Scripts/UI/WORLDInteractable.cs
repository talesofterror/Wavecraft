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
  public float distanceToInteract;

  public Color displayNameColor;

  [HideInInspector] public DialogueTrigger dialogueTrigger;

  public Dispo dispo = new Dispo();
  


  void Awake () {
    if (GetComponent<DialogueTrigger>()) {
      dialogueTrigger = GetComponent<DialogueTrigger>();
    }
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
    Debug.Log("Click Behavior Called");
    if (distanceFromTrigger < distanceToInteract) {
      PLAYERSingleton.i.playerControls.HandleDialogue();
    }
  }

  void OnTriggerExit ()
  {
    NotEngagedBehavior();
  }

  public void NotEngagedBehavior()
  {
    state = InteractableState_WORLD.NotEngaged;
    GAMESingleton.i.engaged_Dialogue = false;
    UISingleton.i.ToggleIdentifierPanel("off");
    UISingleton.i.ToggleDialogue("off");
  }
  
  void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, distanceToInteract);
  }
}

public enum InteractableState_WORLD {
  Hover, 
  Focus,
  NotEngaged
}

public enum Dispo {
  Friendly,
  Hostile,
  Neutral
}