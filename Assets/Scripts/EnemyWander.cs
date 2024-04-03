using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// [ExecuteAlways]
public class EnemyWander : MonoBehaviour
{
  public int numberOfWaypoints;
  public GameObject[] waypointArray;
  List<Vector3> wayPointVectorList;

  Transform parent;

  bool moving = true;

  // Start is called before the first frame update
  void Start()
  {
    wayPointVectorList = new List<Vector3>(numberOfWaypoints);

    for (int wp = 0; wp < waypointArray.Length; wp++)
    {
      waypointArray[wp].transform.parent = null;
      wayPointVectorList.Add(waypointArray[wp].transform.position);
    }


    transform.position = waypointArray[0].transform.position;

    if (Application.isPlaying)
    {
      print("In player or playmode");
    }

  }

  void Update()
  {
    if (moving)
    {
      StartCoroutine(MovementIE());
    }
    else
    {
      StopCoroutine(MovementIE());
    }
  }

  public float lerpSpeed = 0.05f;
  float lerpDriver = 0;
  int lerpState = 0;
  private IEnumerator MovementIE()
  {

    int lerpModulo = lerpState % wayPointVectorList.Count;
    print("lerp modulo = " + lerpModulo);
    lerpDriver += lerpSpeed * Time.deltaTime;
    // transform.position = Vector3.Lerp(wayPointVectorList[0], wayPointVectorList[1], lerpDriver);
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
    if (lerpState > wayPointVectorList.Count - 1) {
      lerpState = 0;
    }
    // int nextWaypoint = lerpState < wayPointVectorList.Count ? modulo + 1 : 0;
    int nextWaypoint = lerpState < wayPointVectorList.Count - 1 ? lerpState + 1: 0;
    print("next waypoint = " + nextWaypoint);
    return Vector3.Lerp(wayPointVectorList[lerpState], wayPointVectorList[nextWaypoint], lerpDriver);
  }

  private void CreatePrimitive(int i)
  {
    waypointArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    waypointArray[i].transform.position = new Vector3(transform.position.x + i + 1, transform.position.y + i + 1, transform.position.z);
    waypointArray[i].transform.parent = transform;
    waypointArray[i].transform.name = "Way Point " + (i + 1);

  }

  // Update is called once per frame


}
