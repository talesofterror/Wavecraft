using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EnemyWander : MonoBehaviour
{
  public GameObject[] waypointArray;
  List<Vector3> wayPointVectorList;
  private float currentLerpDistance;
  public float speed = 0.5f;
  private float lerpSpeed;
  EnemyDamage enemyDamage;

  public Waypoint[] waypoints;
  public WaypointSystem waypointSystem;


  bool moving = true;

  void Start()
  {
    enemyDamage = GetComponent<EnemyDamage>();

    wayPointVectorList = new List<Vector3>(waypointArray.Length);

    waypoints = new Waypoint[waypointArray.Length];

    for (int wp = 0; wp < waypointArray.Length; wp++)
    {
      waypointArray[wp].transform.parent = null;
      wayPointVectorList.Add(waypointArray[wp].transform.position);
      // ! new
      waypoints[wp] = waypointArray[wp].GetComponent<Waypoint>();
    }

    transform.position = wayPointVectorList[0];

    lerpSpeed = speed / 10;

    // * new stuff 
    waypointSystem = new WaypointSystem(waypoints);
    // print(gameObject.name + " Waypoint system array length: " + waypointSystem.waypointGroupArray.Length);
    // print(gameObject.name + " Waypoint system total group distance: " + waypointSystem.totalGroupDistance);
    // print(waypointArray[0].name + " Waypoint[0] distance from starting point: " + waypointSystem.waypointGroupArray[0].distanceFromStart);
    // print(waypointArray[1].name + " Waypoint[1] distance from starting point: " + waypointSystem.waypointGroupArray[1].distanceFromStart);
    // print(waypointArray[2].name + " Waypoint[2] distance from starting point: " + waypointSystem.waypointGroupArray[2].distanceFromStart);
    // print(gameObject.name + " distance Waypoint[2] to Waypoint[0]: " + Vector3.Distance(waypoints[2].position, waypoints[0].position));
  }

  

  void Update()
  {
    if (enemyDamage.dead == true)
    {
      moving = false;
    }
    else
    {
      moving = true;
    }
    if (moving)
    {
      StartCoroutine(MovementIE());
    }
    else
    {
      StopCoroutine(MovementIE());
    }
  }

  float lerpDriver = 0;
  int lerpState = 0;
  private IEnumerator MovementIE()
  {

    lerpDriver += (lerpSpeed * Time.deltaTime);
    transform.position = Lerpinate();
    yield return null;
  }

  Vector3 Lerpinate()
  {
    if (lerpDriver >= 1)
    {
      lerpState++;
      lerpDriver = 0;
    }
    if (lerpState > wayPointVectorList.Count - 1)
    {
      lerpState = 0;
    }
    int nextWaypoint = lerpState < wayPointVectorList.Count - 1 ? lerpState + 1 : 0;
    currentLerpDistance = Vector3.Distance( wayPointVectorList[nextWaypoint], wayPointVectorList[lerpState]);
    // print("current LErp Distance:" + currentLerpDistance);
    // print("Lep distance mod Vector :" + currentLerpDistance * (lerpSpeed * Time.deltaTime));

    // Vector3 position;

    // for(int i = 0; i < waypoints.Length; i++) {
    //   if (waypointSystem.calcDistanceScaledTime(lerpSpeed) < waypoints[i].distanceFromStart) {
    //     Vector3 wFrom = waypoints[i^1].position;
    //     Vector3 wTo = waypoints[i].position;
    //     float f = (waypointSystem.calcDistanceScaledTime(lerpSpeed) - waypoints[i^1].distanceFromStart) / (waypoints[i].distanceFromStart - waypoints[i^1].distanceFromStart);
    //     float x = wFrom.x + (wTo.x - wFrom.x) * f;
    //     float y = wFrom.y + (wTo.y - wFrom.y) * f;
    //     // Vector3 position = new Vector3(transform.position.x - x, transform.position.y - y, 0);
    //     position = new Vector3(x, y, PLAYERSingleton.playerSingleton.interactionZ);
    //     transform.position = position;
    //     print(gameObject.name + " wFrom index: " + waypoints[i^1]);
    //     print(gameObject.name + " wTo index: " + waypoints[i]);
    //   }
    // }
    // return position;

    return Vector3.Lerp(wayPointVectorList[lerpState], wayPointVectorList[nextWaypoint], lerpDriver);
    
  }

    void OnDrawGizmosSelected()
  {
    for (int i = 0; i < waypointArray.Length; i++)
    {
      if (i == waypointArray.Length - 1)
      {
        Gizmos.DrawLine(waypointArray[i].transform.position, waypointArray[0].transform.position);
        Handles.Label(waypointArray[i].transform.position, waypointArray[i].name);
        return;
      }
      Gizmos.color = Color.red;
      Gizmos.DrawLine(waypointArray[i].transform.position, waypointArray[i + 1].transform.position);
      Handles.Label(waypointArray[i].transform.position, waypointArray[i].name);
    }
    
  }

}
