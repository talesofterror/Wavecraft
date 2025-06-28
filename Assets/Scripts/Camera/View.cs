using System.Collections;
using TreeEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class View : MonoBehaviour
{
  [HideInInspector]
  public GameObject player;
  public ViewerObject activeView;
  private ViewerObject initialView;
  public FollowState followState;
  Vector3 PlayerPosition;
  Quaternion gimbalQuaternion;
  bool transitioning = false;

  void Start()
  {
    player = PLAYERSingleton.i.gameObject;

    foreach (AreaDefiner definer in CAMERASingleton.i.areasArray)
    {
      Debug.Log("Area Definer iteration: " + definer.name);
      Debug.Log("Player is within bounds: " + definer.PlayerIsWithinBounds());
      if (definer.PlayerIsWithinBounds())
      {
        Debug.Log("Playr is within: " + definer.name);
        activeView = definer.viewComponent.view;
        return;
      }
      else
      {
        continue;
      }
    }

    initialView = new ViewerObject(CAMERASingleton.i.transform.position, CAMERASingleton.i.transform.rotation, Camera.main.fieldOfView);
    initialView.followState = FollowState.Stationary;
    activeView = initialView;
  }

  void Update()
  {
    setActiveView(activeView);
    setActiveFollowBehavior(followState);
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

  private void setActiveView(ViewerObject view)
  {    
    if (view.followState == FollowState.Stationary)
    {
      activeView.setOffsets(0, 0, 0);
    }
    if (view.lookAt) {
      transform.LookAt(PLAYERSingleton.i.transform);
    } else if (!view.lookAt) {
      transform.rotation = view.rotation;
    }
    transform.position = view.position;
    Camera.main.fieldOfView = view.fieldOfView;
    followState = activeView.followState;
  }

  private void setActiveFollowBehavior(FollowState state)
  {
    if (state == FollowState.Stationary)
    {
      activeView.position = new Vector3(
        activeView.position.x,
        activeView.position.y,
        activeView.position.z
        );
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
    transitioning = true;
    StartCoroutine(ViewTransition(start, target, speed));
  }

  IEnumerator ViewTransition(
    ViewerObject start, ViewerObject target,
    float transitionSpeed)
  {
    gimbalQuaternion = CAMERASingleton.i.LookAtGimbal.rotation;
    Vector3 transitoryPosition;
    Quaternion transitoryRotation;
    float transitoryFieldOfView;

    ViewerObject transitoryView = new ViewerObject(Vector3.zero, Quaternion.Euler(0, 0, 0), 0);
    transitoryView.followState = target.followState;
    transitoryView.lookAt = target.lookAt;

    float i;
    // float rotX, rotY, rotZ;

    // rotX = start.rotation.eulerAngles.x;
    // rotY = start.rotation.eulerAngles.y;
    // rotZ = start.rotation.eulerAngles.z;

    Vector3 calculatedTarget(FollowState state)
    {
      if (state == FollowState.Stationary)
      {
        return target.position;
      }
      if (state == FollowState.Vertical)
      {
        return new Vector3(
          target.position.x, 
          player.transform.position.y, 
          target.position.z
          );
      }
      if (state == FollowState.Horizontal)
      {
        return new Vector3(
          player.transform.position.x, 
          target.position.y, 
          target.position.z
        );
      }
      if (state == FollowState.Total)
      {
        return new Vector3(
          player.transform.position.x, 
          player.transform.position.y, 
          target.position.z
        );
      }
      else
      {
        Debug.Log("No valid FollowState");
        return Vector3.zero;
      }
    }

    for (i = 0; i <= 1; i += 1 / transitionSpeed * Time.deltaTime)
    {
      float transitoryangleX = Mathf.LerpAngle(gimbalQuaternion.x, target.rotation.x, i);
      float transitoryangleY = Mathf.LerpAngle(gimbalQuaternion.y, target.rotation.y, i);
      float transitoryangleZ = Mathf.LerpAngle(gimbalQuaternion.z, target.rotation.z, i);

      if(start.lookAt || target.lookAt) {
        transitoryView.rotation.x = Mathf.LerpAngle(gimbalQuaternion.x, transitoryangleX, i);
        transitoryView.rotation.y = Mathf.LerpAngle(gimbalQuaternion.y, transitoryangleY, i);
        transitoryView.rotation.z = Mathf.LerpAngle(gimbalQuaternion.z, transitoryangleZ, i);
      } else if (!start.lookAt) {
        transitoryView.rotation.x = Mathf.LerpAngle(start.rotation.x, target.rotation.x, i);
        transitoryView.rotation.y = Mathf.LerpAngle(start.rotation.y, target.rotation.y, i);
        transitoryView.rotation.z = Mathf.LerpAngle(start.rotation.z, target.rotation.z, i);
      }

      transitoryPosition = Vector3.Lerp(start.position, calculatedTarget(followState), i);
      transitoryRotation = transitoryView.rotation;
      transitoryFieldOfView = Mathf.Lerp(start.fieldOfView, target.fieldOfView, i);

      transitoryView.position = transitoryPosition;
      transitoryView.rotation = transitoryRotation;
      transitoryView.fieldOfView = transitoryFieldOfView;

      activeView = transitoryView;

      // if(i > 1) {
      //   transitioning = false;
      //   StopAllCoroutines();
      // }
      yield return null;
    }
  }
}

public enum ViewerRotationStyle {
  FollowPlayerX, 
  FollowPlayerY, 
  FollowSatyr, 
  FollowBoss
}