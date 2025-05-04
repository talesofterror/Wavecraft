using UnityEngine;
using UnityEngine.Android;

public class InteractionReceiver : MonoBehaviour
{

[HideInInspector] public bool active;
Interactable parentInteractable;

void OnTriggerEnter(Collider collider) {
  if (!parentInteractable) {
   parentInteractable = GetComponentInParent<Interactable>();
  }
  parentInteractable.state = InteractableState_WORLD.Hover;
  parentInteractable.HoverBehavior(collider);
  active = true;
}

void OnTriggerExit () {
  parentInteractable.state = InteractableState_WORLD.NotEngaged;
  parentInteractable.NotEngagedBehavior();
  active = false;
}

}
