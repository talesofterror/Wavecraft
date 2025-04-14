using System.Linq;
using UnityEngine;

public class WaypointSystem 
{
  [SerializeField] public Waypoint[] waypointGroupArray;
  public float totalGroupDistance;

  public WaypointSystem (Waypoint[] waypointArray) {
    this.waypointGroupArray = waypointArray;
    for (int i = 0; i < waypointGroupArray.Length; i++) {
      this.waypointGroupArray[i].distanceFromStart = calcDistanceFromStart(i, this.waypointGroupArray);
    }
    this.totalGroupDistance = calcTotalGroupDistance(this.waypointGroupArray);
  }

  public float calcDistanceFromStart (int i, Waypoint[] group) {
    float d =0;
    for (int w = i; w <= group.Length - 1; w++) {
      if (w == group.Length - 1) {
        d += Vector3.Distance(group[w].position, group[0].position);
        continue;
      }
      d += Vector3.Distance(group[w].position, group[w+1].position);
    }
    return d;
  }

  private float calcTotalGroupDistance (Waypoint[] group) {
    float d = 0;
    for (int w = 0; w <= group.Length-1; w++) {
      // Debug.Log("processing " + group[w].transform.parent.name + " - " + group[w].name);
      if (w == group.Length-1) {
        d+=Vector3.Distance(group[w].position, group[0].position);
        break;
      }
      d += Vector3.Distance(group[w].position, group[w+1].position);
    }
    return d;
  }

  public float calcDistanceScaledTime (float speed) {
    float d = 0;
    float t = Time.time;
    d = Mathf.Sin((t * (speed)) % totalGroupDistance);
    return d;
  }
}