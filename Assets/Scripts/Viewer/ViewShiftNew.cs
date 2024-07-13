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
  public FollowState targetFollowState;
  public bool targetLookAt;
  private ViewerObject targetView;
  private ViewerObject initView;
  private bool toggle = false;
  public bool editMode;
  void Awake()
  {
    targetView = new ViewerObject(targetPosition, targetRotation, targetFieldOfView);
  }
  void Start()
  {
    targetView.followState = targetFollowState;
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.CompareTag("GuyBase"))
    {
      if (!toggle)
      {
        targetView.position = targetPosition;
        targetView.rotation = targetRotation;
        targetView.fieldOfView = targetFieldOfView;
        targetView.followState = targetFollowState;
        initView = Camera.main.gameObject.GetComponent<ViewerRevised>().activeView;
        print("Entry Trigger follow state: " + targetFollowState);
        Camera.main.gameObject.GetComponent<ViewerRevised>().activeView = targetView;
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
    {
      targetView.position = targetPosition;
      targetView.rotation = targetRotation;
      targetView.fieldOfView = targetFieldOfView;
      targetView.followState = targetFollowState;
    }
  }
}
