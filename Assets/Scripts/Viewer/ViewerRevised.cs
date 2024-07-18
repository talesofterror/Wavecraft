using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

/* 
! outline

Needs flexible coroutine for transitions
Moving player into new area should initiate relevant camera rules for area

^ Private variables


^ Public variables to be set by triggers and other code:
(Should be set within each state)
  ? State
  ? X Offset from player
  ? Y Position
  ? X Rotation
  ? Y Rotation
  ? Z Rotation
  ? FOV
  * Combine into class?
    (instantiate object instance, set into variable within Update

^ States: 
(Each takes their offsets and FOV from public variables)
  * Follow Player: 
    ? Vertical
    ? Horizontal
  * Stationary

^ Functions: 
  * SwitchView
  * ViewpointTransition Coroutine (May be the same as SwitchView)
  * MotionLead (Shift camera in direction of movement)

*/

public class ViewerRevised : MonoBehaviour
{

  public GameObject player;
  public ViewerObject activeView;
  private ViewerObject initialView;
  public FollowState followState;

  void Awake()
  {
    player = GameObject.FindWithTag("GuyBase");
    initialView = new ViewerObject(transform.position, transform.rotation, Camera.main.fieldOfView);
    initialView.setOffsets(0.0f, 3.7f, 0.02f);
    initialView.setFollowState(followState);
    activeView = initialView;
  }

  void Update()
  {
    setActiveView(activeView);
    setFollowBehavior(followState);
    // debug();
  }

  void FixedUpdate()
  {
  }

  void debug()
  {
    print("active view position: " + activeView.position);
    print("active view rotation: " + activeView.rotation);
  }

  void setActiveView(ViewerObject view)
  {
    if (view.followState == FollowState.Stationary)
    {
      activeView.setOffsets(0, 0, 0);
    }
    transform.position = view.position + view.offsets;
    transform.rotation = view.rotation;
    Camera.main.fieldOfView = view.fieldOfView;
    followState = activeView.followState;
  }

  public void setFollowBehavior(FollowState state)
  {
    if (state == FollowState.Stationary)
    {
      activeView.position = new Vector3(
        activeView.position.x,
        activeView.position.y,
        activeView.position.z
        );
      Debug.Log("follow state set stationary");
    }
    if (state == FollowState.Vertical)
    {
      activeView.position = new Vector3(
        activeView.position.x,
        player.transform.position.y,
        activeView.position.z
        );
    }
    if (state == FollowState.Horizontal)
    {
      activeView.position = new Vector3(
        player.transform.position.x,
        activeView.position.y,
        activeView.position.z
        );
    }
    if (state == FollowState.Total)
    {
      activeView.position = new Vector3(
        player.transform.position.x,
        player.transform.position.y,
        activeView.position.z
        );
    }
  }

  public void callViewTransition(ViewerObject start, ViewerObject target, float speed)
  {
    StartCoroutine(ViewTransition(start, target, speed));
  }

  IEnumerator ViewTransition(
    ViewerObject start, ViewerObject target,
    float transitionSpeed)
  {
    Vector3 transitoryPosition;
    Quaternion transitoryRotation;
    float transitoryFieldOfView;

    ViewerObject transitoryView = new ViewerObject(Vector3.zero, Quaternion.Euler(0, 0, 0), 0);

    float i;
    float rotX, rotY, rotZ;

    rotX = start.rotation.eulerAngles.x;
    rotY = start.rotation.eulerAngles.y;
    rotZ = start.rotation.eulerAngles.z;

    Vector3 calculatedTarget(FollowState state)
    {
      if (state == FollowState.Stationary)
      {
        return target.position;
      }
      if (state == FollowState.Vertical)
      {
        return new Vector3(target.position.x, player.transform.position.y, target.position.z);
      }
      if (state == FollowState.Horizontal)
      {
        return new Vector3(player.transform.position.x, target.position.y, target.position.z);
      }
      if (state == FollowState.Total)
      {
        return new Vector3(player.transform.position.x, player.transform.position.y, target.position.z);
      }
      else
      {
        Debug.Log("No valid FollowState");
        return Vector3.zero;
      }
    }

    for (i = 0; i < 1; i += 1 / transitionSpeed * Time.deltaTime)
    {
      rotX = Mathf.Lerp(rotX, target.rotation.x, i);
      rotY = Mathf.Lerp(rotY, target.rotation.y, i);
      rotZ = Mathf.Lerp(rotZ, target.rotation.z, i);

      transitoryPosition = Vector3.Lerp(start.position, calculatedTarget(followState), i);
      transitoryRotation = Quaternion.Euler(rotX, rotY, rotZ);
      transitoryFieldOfView = Mathf.Lerp(start.fieldOfView, target.fieldOfView, i);

      transitoryView.position = transitoryPosition;
      transitoryView.rotation = transitoryRotation;
      transitoryView.fieldOfView = transitoryFieldOfView;

      activeView = transitoryView;
      yield return null;
    }
  }
}
