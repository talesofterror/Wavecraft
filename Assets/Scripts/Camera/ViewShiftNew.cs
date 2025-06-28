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
  public Vector3 position;
  public Quaternion rotation;
  public float fieldOfView;
  public Vector3 offsets;
  public FollowState followState;
  public bool lookAtState;
  private View mainCamViewerRevised;
  [HideInInspector] public ViewerObject shiftView;
  private ViewerObject initView;
  [HideInInspector] public bool triggeredAlready = false;
  public bool editMode;
  public bool enableTransition = true;
  void Awake()
  {
    shiftView = new ViewerObject(position, rotation, fieldOfView);
    shiftView.setOffsets(offsets.x, offsets.y, offsets.z);
    shiftView.setFollowState(followState);
    shiftView.setLookAt(lookAtState);
    mainCamViewerRevised = Camera.main.gameObject.GetComponent<View>();
    editMode = false;
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
          CAMERASingleton.i.viewerScript.callViewTransition(initView, shiftView, 1.5f);
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
          mainCamViewerRevised.callViewTransition(mainCamViewerRevised.activeView, initView, 1.5f);
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
      // mainCamViewerRevised.activeView.position = calculatedTarget(mainCamViewerRevised.followState);
      mainCamViewerRevised.activeView.position = position;
      mainCamViewerRevised.activeView.rotation = rotation;
      mainCamViewerRevised.activeView.fieldOfView = fieldOfView;
      mainCamViewerRevised.activeView.followState = followState;
      mainCamViewerRevised.activeView.offsets = offsets;
    }
  }

  Vector3 calculatedTarget(FollowState state)
  {
    if (state == FollowState.Stationary)
    {
      return position;
    }
    if (state == FollowState.Vertical)
    {
      return new Vector3(
        position.x,
        mainCamViewerRevised.player.transform.position.y,
        position.z
        );
    }
    if (state == FollowState.Horizontal)
    {
      return new Vector3(
        mainCamViewerRevised.player.transform.position.x,
        position.y,
        position.z
        );
    }
    if (state == FollowState.Total)
    {
      return new Vector3(
        mainCamViewerRevised.player.transform.position.x,
        mainCamViewerRevised.player.transform.position.y,
        position.z
        );
    }
    else
    {
      Debug.Log("No valid FollowState");
      return Vector3.zero;
    }
  }
}
