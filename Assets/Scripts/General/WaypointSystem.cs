using System.Linq;
using UnityEngine;

public class WaypointSystem 
{
  [SerializeField] public Waypoint[] waypointGroupArray;
  public float totalGroupDistance;

  public WaypointSystem (Waypoint[] waypointArray) {
    waypointGroupArray = waypointArray;
    for (int i = 0; i < waypointGroupArray.Length; i++) {
      waypointGroupArray[i].distanceFromStart = calculateDistanceFromStart(i, waypointGroupArray);
    }
    totalGroupDistance = calcTotalGroupDistance(waypointGroupArray);
  }

  private float calculateDistanceFromStart (int i, Waypoint[] group) {
    float d =0;

    for (int w = i; w <= group.Length - 1; w++) {
      if (w == group.Length - 1) {
        d += Vector3.Distance(group[w].position, group[0].position);
        break;
      }
      d += Vector3.Distance(group[w].position, group[w+1].position);
    }

    return d;
  }

  float calcTotalGroupDistance (Waypoint[] group) {
    float d = 0;
    for (int w = 0; w < group.Length - 1; w++) {
      if (w == group.Length - 1) {
        d += Vector3.Distance(group[w].position, group[0].position);
        break;
      }
      d += Vector3.Distance(group[w].position, group[w+1].position);
    }
    return d;
  }

  private float calculateTimeScaledDistance (Waypoint[] group) {
    float d =0;

    return d;
  }
}
