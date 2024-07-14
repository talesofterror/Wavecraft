using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
TODO 

? clamp X, clamp Y
  (set here, impliment in ViewerRevised)
  (limits movement of camera beyond a certain point while triggered view is active)
? active camera area
  (need to define an area, set camera rules for area based on player transform at Awake)

*/

public class ViewShiftNew : MonoBehaviour
{
  public Vector3 targetPosition;
  public Quaternion targetRotation;
  public float targetFieldOfView;
  public Vector3 offsets;
  public FollowState targetFollowState;
  public bool targetLookAt;
  private ViewerRevised mainCamViewerRevised;
  private ViewerObject shiftView;
  private ViewerObject initView;
  private bool toggle = false;
  public bool editMode;
  public bool enableTransition = true;
  void Awake()
  {
    shiftView = new ViewerObject(targetPosition, targetRotation, targetFieldOfView);
    shiftView.setOffsets(offsets.x, offsets.y, offsets.z);
    shiftView.setFollowState(targetFollowState);
    mainCamViewerRevised = Camera.main.gameObject.GetComponent<ViewerRevised>();
  }
  void Start()
  {
    print("shiftView follow state: " + shiftView.followState);
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.CompareTag("GuyBase"))
    {
      if (!toggle)
      {
        if (enableTransition)
        {
          initView = mainCamViewerRevised.activeView;
          mainCamViewerRevised.callViewTransition(mainCamViewerRevised.activeView, shiftView, 1.5f);
          mainCamViewerRevised.activeView.followState = shiftView.followState;
          mainCamViewerRevised.activeView.offsets = shiftView.offsets;
        }
        else
        {
        initView = mainCamViewerRevised.activeView;
        mainCamViewerRevised.activeView = shiftView;
        }
        toggle = true;
      }
      else
      {
        if (enableTransition)
        {
          mainCamViewerRevised.callViewTransition(mainCamViewerRevised.activeView, initView, 1.5f);
          mainCamViewerRevised.activeView.followState = initView.followState;
          mainCamViewerRevised.activeView.offsets = initView.offsets;
        }
        else
        {
        Camera.main.gameObject.GetComponent<ViewerRevised>().activeView = initView;
        }
        toggle = false;
      }
    }

  }
  void Update()
  {
    if (editMode)
    {
      shiftView.position = targetPosition;
      shiftView.rotation = targetRotation;
      shiftView.fieldOfView = targetFieldOfView;
      shiftView.followState = targetFollowState;
      shiftView.offsets = offsets;
    }
  }
}
