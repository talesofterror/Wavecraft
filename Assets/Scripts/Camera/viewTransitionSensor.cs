using UnityEngine;

public class viewTransitionSensor : MonoBehaviour
{
  ViewShiftv2 viewShiftScript;


  void Start()
  {
    viewShiftScript = GetComponentInParent<ViewShiftv2>();
  }

  void Update()
  {

  }

  void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.CompareTag("GuyBase"))
    {
      viewShiftScript.triggerViewTransition(transform.name);
    }
  }
}
