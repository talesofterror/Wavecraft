using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyWander : MonoBehaviour
{
  public int numberOfWaypoints;
  public GameObject[] waypointArray;
  List<Vector3> wayPointVectorList;
  private float currentLerpDistance;

  EnemyDamage enemyDamage;

  bool moving = true;

  void Start()
  {
    enemyDamage = GetComponent<EnemyDamage>();

    wayPointVectorList = new List<Vector3>(numberOfWaypoints);

    for (int wp = 0; wp < waypointArray.Length; wp++)
    {
      waypointArray[wp].transform.parent = null;
      wayPointVectorList.Add(waypointArray[wp].transform.position);
    }

    transform.position = waypointArray[0].transform.position;
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

  public float lerpSpeed = 0.5f;
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
    return Vector3.Lerp(wayPointVectorList[lerpState], wayPointVectorList[nextWaypoint], lerpDriver);
  }

  private void CreatePrimitive(int i)
  {
    waypointArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    waypointArray[i].transform.position = new Vector3(transform.position.x + i + 1, transform.position.y + i + 1, transform.position.z);
    waypointArray[i].transform.parent = transform;
    waypointArray[i].transform.name = "Way Point " + (i + 1);
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
