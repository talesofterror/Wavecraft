using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
! outline

Needs flexible coroutine for transitions

^ Public variables to be set by triggers and other code:
(Should be set within each state)
  ? State
  ? X Offset from player
  ? Y Position
  ? X Rotation
  ? Y Rotation
  ? Z Rotation
  ? FOV

^ States: 
(Each takes their offsets and FOV from public variables)
  * Follow Player: 
    ? Vertical
    ? Horizontal
  * Stationary

^ Functions: 
  * Viewpoint Trasition Coroutine

*/

public class ViewerRevised : MonoBehaviour
{

  private GameObject player;
  private Vector3 playerPosition;
  private Vector3 origPosition;
  public enum ViewerState
  {
    Stationary,
    Horizontal,
    Vertical
  }

  // Start is called before the first frame update
  void Start()
  {
    origPosition = transform.position;
    player = GameObject.FindWithTag("GuyBase");
  }

  // Update is called once per frame
  void Update()
  {
    playerPosition = player.transform.position;
    transform.position = new Vector3(origPosition.x, playerPosition.y, origPosition.z);
  }

  void FixedUpdate()
  {
  }
}
