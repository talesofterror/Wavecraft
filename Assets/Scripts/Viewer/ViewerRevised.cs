using System.Collections;
using UnityEngine;

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

  private GameObject player;
  public ViewerObject activeView;
  private ViewerObject initialView;
  public FollowState followState;

  // Start is called before the first frame update
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
    setFollowBehavior(activeView.followState);
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
    if (followState == FollowState.Stationary)
    {
      activeView.setOffsets(0, 0, 0);
    }
    transform.position = view.position + view.offsets;
    transform.rotation = view.rotation;
    Camera.main.fieldOfView = view.fieldOfView;
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

  IEnumerator ViewTransition(
    ViewerObject start, ViewerObject target,
    float transitionLength, float transitionSpeed)
  {
    Vector3 transitoryPosition;
    Quaternion transitoryRotation;
    float transitoryFieldOfView;

    ViewerObject transitoryView = new ViewerObject(
      start.position, start.rotation,
      start.fieldOfView
    );

    float i;
    for (i = 0; i < transitionLength; i += transitionSpeed / 1 * Time.deltaTime)
    {
      activeView = transitoryView;
      yield return null;
    }
  }
}
