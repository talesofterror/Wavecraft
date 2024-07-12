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
  private ViewerObject activeView;
  private ViewerObject initialView;

  // Start is called before the first frame update
  void Start()
  {
    initialView = new ViewerObject(transform.position, transform.rotation, Camera.main.fieldOfView);
    activeView = initialView;
    activeView.setFollowState(FollowState.Vertical);
    player = GameObject.FindWithTag("GuyBase");
  }

  void Update()
  {
    setActiveView(activeView);
    setFollowBehavior(activeView.followState);
    debug();
  }

  void debug()
  {
    print("active view position: " + activeView.position);
    print("active view rotation: " + activeView.rotation);
  }

  void setActiveView(ViewerObject view)
  {
    transform.position = view.position;
    transform.rotation = view.rotation;
    Camera.main.fieldOfView = view.fieldOfView;
  }

  [HideInInspector]
  public float followStateXOffset;
  [HideInInspector]
  public float followStateYOffset;

  void setFollowBehavior(FollowState state)
  {
    if (state == FollowState.Stationary)
    {
      return;
    }
    if (state == FollowState.Vertical)
    {
      activeView.position = new Vector3(
        activeView.position.x + followStateXOffset,
        player.transform.position.y,
        activeView.position.z
        );
    }
    if (state == FollowState.Horizontal)
    {
      activeView.position = new Vector3(
        player.transform.position.x,
        activeView.position.y + followStateYOffset,
        activeView.position.z
        );
    }
    if (state == FollowState.Total)
    {
      activeView.position = new Vector3(
        activeView.position.x + followStateXOffset,
        player.transform.position.y + followStateYOffset,
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
