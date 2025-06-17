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

  int initPoint = 0;
  public int startPointOffset = 0;


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
    initPoint = waypointArray.Length % startPointOffset;
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
      initPoint++;
      lerpDriver = 0;
    }
    if (initPoint > wayPointVectorList.Count - 1)
    {
      initPoint = 0;
    }
    int nextWaypoint = initPoint < wayPointVectorList.Count - 1 ? initPoint + 1 : 0;
    currentLerpDistance = Vector3.Distance( wayPointVectorList[nextWaypoint], wayPointVectorList[initPoint]);
    // print("current LErp Distance:" + currentLerpDistance);
    // print("Lep distance mod Vector :" + currentLerpDistance * (lerpSpeed * Time.deltaTime));

    return Vector3.Lerp(wayPointVectorList[initPoint], wayPointVectorList[nextWaypoint], lerpDriver);
    
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
