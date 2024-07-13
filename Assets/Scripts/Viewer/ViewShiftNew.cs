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
  private ViewerObject shiftView;
  private ViewerObject initView;
  private bool toggle = false;
  public bool editMode;
  void Awake()
  {
    shiftView = new ViewerObject(targetPosition, targetRotation, targetFieldOfView);
    shiftView.setOffsets(offsets.x, offsets.y, offsets.z);
  }
  void Start()
  {
    shiftView.followState = targetFollowState;
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.CompareTag("GuyBase"))
    {
      if (!toggle)
      {
        // shiftView.position = targetPosition;
        // shiftView.rotation = targetRotation;
        // shiftView.fieldOfView = targetFieldOfView;
        // shiftView.followState = targetFollowState;
        initView = Camera.main.gameObject.GetComponent<ViewerRevised>().activeView;
        print("Entry Trigger follow state: " + targetFollowState);
        Camera.main.gameObject.GetComponent<ViewerRevised>().activeView = shiftView;
        toggle = true;
      }
      else
      {
        Camera.main.gameObject.GetComponent<ViewerRevised>().activeView = initView;
        toggle = false;
      }
    }

  }
  void Update()
  {
    if (editMode)
    // & Edit mode should integrate ViewerObject.offsets
    {
      shiftView.position = targetPosition;
      shiftView.rotation = targetRotation;
      shiftView.fieldOfView = targetFieldOfView;
      shiftView.followState = targetFollowState;
      shiftView.offsets = offsets;
    }
  }
}
