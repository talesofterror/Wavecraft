using System.Collections;
using UnityEngine;

public class ViewShiftv2 : MonoBehaviour
{
  public GameObject trigger1;
  ViewerObject trigger1ViewObject;
  Collider trigger1Collider;
  public GameObject trigger2;
  ViewerObject trigger2ViewObject;
  Collider trigger2Collider;
  [SerializeField] float nudgeForce = 10f;

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
    trigger1ViewObject = trigger1.GetComponent<ViewComponent>().view;
    trigger1Collider = trigger1.GetComponent<Collider>();
    trigger2ViewObject = trigger2.GetComponent<ViewComponent>().view;
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
        CAMERASingleton.i.viewerScript.callViewTransition(activeView, trigger1ViewObject, 1.5f);
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
        CAMERASingleton.i.viewerScript.callViewTransition(activeView, trigger2ViewObject, 1.5f);
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
    PLAYERSingleton.i.playerControls.sprinting = false;
    nudgePlayer();
    yield return new WaitForSeconds(1f);
    trigger1Collider.enabled = true;
    trigger2Collider.enabled = true;
    PLAYERSingleton.i.controlsActive = true;
    PLAYERSingleton.i.playerControls.sprinting = false;
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

  // private void OnTriggerEnter(Collider collider)
  // {
  //   if (collider.gameObject.CompareTag("GuyBase"))
  //   {
  //     if (!triggeredAlready)
  //     {
  //       if (enableTransition)
  //       {
  //         initView = mainCamViewerRevised.activeView;
  //         CAMERASingleton.i.viewerScript.callViewTransition(initView, shiftView, 1.5f);
  //       }
  //       else
  //       {
  //         initView = mainCamViewerRevised.activeView;
  //         mainCamViewerRevised.activeView = shiftView;
  //       }
  //       triggeredAlready = true;
  //     }
  //     else
  //     {
  //       if (enableTransition)
  //       {
  //         mainCamViewerRevised.callViewTransition(mainCamViewerRevised.activeView, initView, 1.5f);
  //       }
  //       else
  //       {
  //         mainCamViewerRevised.activeView = initView;
  //       }
  //       triggeredAlready = false;
  //     }
  //   }

  // }
}
