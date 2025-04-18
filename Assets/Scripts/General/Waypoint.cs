using UnityEngine;

public class Waypoint : MonoBehaviour
{
  [HideInInspector] public Waypoint[] waypointGroup;
  [HideInInspector] public float distanceFromStart;
  [HideInInspector] public Vector3 position;

  void Awake()
  {
    position = transform.position;
  }
}
