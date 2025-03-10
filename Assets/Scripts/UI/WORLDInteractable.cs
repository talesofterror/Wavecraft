using UnityEngine;

public class WORLDInteractable : MonoBehaviour
{
  public string _tag;
  public string _name;
  public Vector3 position;
  public InteractableState_WORLD state = InteractableState_WORLD.NotEngaged;
  public bool isInRange;
  private float distance;

  public GameObject interactableObject;

    void Awake () {
      // interactableObject.transform.localPosition = interactableObject.transform.localPosition
      //   + new Vector3(0, 0, PLAYERSingleton.playerSingleton.interactionZ);
    }

    void Start()
    {
        _name = gameObject.name;
        _tag = gameObject.tag;
        position = gameObject.transform.position;
    }
    
    void Update()
    {
      if (state == InteractableState_WORLD.Hover) {
        ClickBehavior(); 
      }
      if (state == InteractableState_WORLD.Focus) {
        if (Input.GetKeyDown(KeyCode.Escape)) {

        }
      }
    }

  void OnTriggerEnter(Collider other) {
    if (state == InteractableState_WORLD.NotEngaged)
    {
      HoverBehavior(other);
    }
  }

  private void HoverBehavior(Collider other)
  {
    if (other.CompareTag("Pointer"))
    {
      state = InteractableState_WORLD.Hover;
      print(this);
      UISingleton.uiSingleton.cursorTarget = this;
      UISingleton.uiSingleton._cursor.UpdateAppearance(this);
      UISingleton.uiSingleton.ToggleIdentifierPanel("on");
      UISingleton.uiSingleton.IdentifierText.text = _name;
      print("Interactable: " + _name + ".");
    }

  }

  private void ClickBehavior () {
    if (Input.GetMouseButtonDown(0)) {
      print("Interactable ClickBehavior() called");
      state = InteractableState_WORLD.Focus;
      UISingleton.uiSingleton.ToggleDialogue("on");
    }
  }

  void OnTriggerExit ()
  {
    NotEngagedBehavior();
  }

  private void NotEngagedBehavior()
  {
    state = InteractableState_WORLD.NotEngaged;
    UISingleton.uiSingleton.ToggleIdentifierPanel("off");
    UISingleton.uiSingleton.ToggleDialogue("off");
  }
}

public enum InteractableState_WORLD {
  Hover, 
  Focus,
  NotEngaged
}
