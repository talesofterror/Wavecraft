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
  private bool triggeredAlready = false;
  public bool editMode;
  public bool enableTransition = true;
  void Awake()
  {
    shiftView = new ViewerObject(targetPosition, targetRotation, targetFieldOfView);
    shiftView.setOffsets(offsets.x, offsets.y, offsets.z);
    shiftView.setFollowState(targetFollowState);
    mainCamViewerRevised = Camera.main.gameObject.GetComponent<ViewerRevised>();
    editMode = false;
  }
  void Start()
  {
    print("shiftView follow state: " + shiftView.followState);
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.CompareTag("GuyBase"))
    {
      if (!triggeredAlready)
      {
        if (enableTransition)
        {
          initView = mainCamViewerRevised.activeView;
          mainCamViewerRevised.callViewTransition(initView, shiftView, 1.5f);
          mainCamViewerRevised.activeView.followState = shiftView.followState;
          mainCamViewerRevised.activeView.offsets = shiftView.offsets;
        }
        else
        {
          initView = mainCamViewerRevised.activeView;
          mainCamViewerRevised.activeView = shiftView;
        }
        triggeredAlready = true;
      }
      else
      {
        if (enableTransition)
        {
          mainCamViewerRevised.activeView.offsets = initView.offsets;
          mainCamViewerRevised.callViewTransition(mainCamViewerRevised.activeView, initView, 1.5f);
          mainCamViewerRevised.activeView.followState = initView.followState;
        }
        else
        {
          mainCamViewerRevised.activeView = initView;
        }
        triggeredAlready = false;
      }
    }

  }
  void Update()
  {
    if (editMode)
    {
      mainCamViewerRevised.activeView.position = calculatedTarget(mainCamViewerRevised.followState);
      mainCamViewerRevised.activeView.rotation = targetRotation;
      mainCamViewerRevised.activeView.fieldOfView = targetFieldOfView;
      mainCamViewerRevised.activeView.followState = targetFollowState;
      mainCamViewerRevised.activeView.offsets = offsets;
    }
  }

  Vector3 calculatedTarget(FollowState state)
  {
    if (state == FollowState.Stationary)
    {
      return targetPosition;
    }
    if (state == FollowState.Vertical)
    {
      return new Vector3(
        targetPosition.x,
        mainCamViewerRevised.player.transform.position.y,
        targetPosition.z
        );
    }
    if (state == FollowState.Horizontal)
    {
      return new Vector3(
        mainCamViewerRevised.player.transform.position.x,
        targetPosition.y,
        targetPosition.z
        );
    }
    if (state == FollowState.Total)
    {
      return new Vector3(
        mainCamViewerRevised.player.transform.position.x,
        mainCamViewerRevised.player.transform.position.y,
        targetPosition.z
        );
    }
    else
    {
      Debug.Log("No valid FollowState");
      return Vector3.zero;
    }
  }
}
