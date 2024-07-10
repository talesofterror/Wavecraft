using System.Collections;
using System.Collections.Generic;
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

*/

public class ViewerRevised : MonoBehaviour
{

  private GameObject player;
  private Vector3 origPosition;
  private ViewerObject activeView;

  // Start is called before the first frame update
  void Start()
  {
    origPosition = transform.position;
    activeView = new ViewerObject(origPosition, transform.rotation, 28.7f);
    activeView.setFollowState("vertical");
    player = GameObject.FindWithTag("GuyBase");
  }

  // Update is called once per frame
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

  void setFollowBehavior(ViewerObject.FollowState state)
  {
    if (state == ViewerObject.FollowState.Stationary)
    {
      return;
    }
    if (state == ViewerObject.FollowState.Vertical)
    {
      activeView.position = new Vector3(
        activeView.position.x,
        player.transform.position.y,
        activeView.position.z
        );
    }
    if (state == ViewerObject.FollowState.Horizontal)
    {
      activeView.position = new Vector3(
        player.transform.position.x,
        activeView.position.y,
        activeView.position.z
        );
    }
    if (state == ViewerObject.FollowState.Total)
    {
      activeView.position = new Vector3(
        activeView.position.x,
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
