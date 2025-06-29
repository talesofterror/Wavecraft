using System.Collections;
using UnityEngine;

public class ViewShiftv2 : MonoBehaviour
{
  public GameObject trigger1;
  public ViewComponent trigger1ViewComponent;
  ViewerObject trigger1ViewObject;
  Collider trigger1Collider;
  public GameObject trigger2;
  public ViewComponent trigger2ViewComponent;
  ViewerObject trigger2ViewObject;
  Collider trigger2Collider;
  [SerializeField] float nudgeForce = 10f;
  float transitionSpeed = 1.5f;

  public bool enableTransition = true;
  public bool editMode;

  public enum ActiveTrigger
  {
    trigger1,
    trigger2
  }

  public ActiveTrigger activeTrigger = ActiveTrigger.trigger1;

  void Start()
  {
    if (trigger1ViewComponent)
    {
      trigger1ViewObject = trigger1ViewComponent.view;
    }
    else
    {
      trigger1ViewObject = trigger1.GetComponent<ViewComponent>().view;
    }
    if (trigger2ViewComponent)
    {
      trigger2ViewObject = trigger2ViewComponent.view;
    }
    else
    {
      trigger2ViewObject = trigger2.GetComponent<ViewComponent>().view;
    }

    trigger1Collider = trigger1.GetComponent<Collider>();
    trigger2Collider = trigger2.GetComponent<Collider>();

    editMode = false;
  }

  void Update()
  {
    if (editMode)
    {
      if (activeTrigger == ActiveTrigger.trigger1)
      {
        CAMERASingleton.i.viewerScript.activeView = trigger1ViewObject;
      }
      if (activeTrigger == ActiveTrigger.trigger2)
      {
        CAMERASingleton.i.viewerScript.activeView = trigger2ViewObject;
      }
    }
  }

  public void triggerViewTransition(string trigger)
  {
    Debug.Log("trigger view transition called");
    Debug.Log("trigger = " + trigger);
    ViewerObject activeView = CAMERASingleton.i.viewerScript.activeView;

    if (trigger == "Trigger 1")
    {
      Debug.Log(transform.name + " " + trigger);
      if (enableTransition)
      {
        CAMERASingleton.i.viewerScript.callViewTransition(activeView, trigger1ViewObject, transitionSpeed);
      }
      else
      {
        activeView = trigger1ViewObject;
      }
      activeTrigger = ActiveTrigger.trigger1;
    }
    else if (trigger == "Trigger 2")
    {
      Debug.Log(transform.name + " " + trigger);
      if (enableTransition)
      {
        CAMERASingleton.i.viewerScript.callViewTransition(activeView, trigger2ViewObject, transitionSpeed);
      }
      else
      {
        activeView = trigger2ViewObject;
      }
      activeTrigger = ActiveTrigger.trigger2;
    }

    StartCoroutine(flashColliders());
  }

  IEnumerator flashColliders()
  {
    trigger1Collider.enabled = false;
    trigger2Collider.enabled = false;
    PLAYERSingleton.i.controlsActive = false;
    PLAYERSingleton.i.areaTransition = true;
    nudgePlayer();
    yield return new WaitForSeconds(transitionSpeed);
    PLAYERSingleton.i.rB.linearVelocity *= 0.5f;
    trigger1Collider.enabled = true;
    trigger2Collider.enabled = true;
    PLAYERSingleton.i.controlsActive = true;
    PLAYERSingleton.i.areaTransition = false;
  }

  void nudgePlayer()
  {
    
    if (activeTrigger == ActiveTrigger.trigger1)
    {
      PLAYERSingleton.i.rB.linearVelocity += UTILITY.getDirectionVector3(
        trigger1.transform.position, trigger2.transform.position
        ) * nudgeForce;
    }
    else
    {
      PLAYERSingleton.i.rB.linearVelocity += UTILITY.getDirectionVector3(
        trigger2.transform.position, trigger1.transform.position
        ) * nudgeForce;
    }
  }

}
