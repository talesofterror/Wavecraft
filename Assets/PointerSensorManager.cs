using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSensorManager : MonoBehaviour
{
  public Camera cam;
  LayerMask sensorLayer;
  public RaycastHit rayhit;
  public GameObject cursorTarget;
  public GameObject sensorTarget;

  public GameObject player;

  void Awake()
  {

    Cursor.visible = false;
    cursorTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    cursorTarget.name = "****** Cursor Target!";
    cursorTarget.GetComponent<Collider>().enabled = false;
    cursorTarget.layer = 5;

    sensorTarget = GameObject.CreatePrimitive(PrimitiveType.Cube);
    sensorTarget.name = "****** Sensor Target!";
    sensorTarget.GetComponent<Collider>().enabled = false;
    sensorTarget.layer = 5;

    sensorLayer = 1 << 6;
  }

  void Update()
  {
    Ray rayForSensor = cam.ScreenPointToRay(Input.mousePosition);

    // Ray Projection Attempt -- not working //
    // Ray projectionRay = new Ray(playerObject.transform.position - transform.position, transform.position);
    // Vector3 projectionPlayerPoint = projectionRay.GetPoint(Vector3.Distance(transform.position, playerObject.transform.position));
    // Vector3 projectionSensorPoint = rayForSensor.GetPoint(playerObject.transform.position.z);
    // Vector3 projection = Vector3.Project(projectionSensorPoint, projectionPlayerPoint);
    // debugSphere.transform.position = projection;

    if (Physics.Raycast(rayForSensor.origin, rayForSensor.direction * 100, out rayhit, Mathf.Infinity, sensorLayer))
    {
      Debug.DrawRay(rayForSensor.origin, rayForSensor.direction * 100, Color.red);

      Vector3 rayhitMinusZ = new Vector3(rayhit.point.x, rayhit.point.y, player.transform.position.z);

      cursorTarget.transform.position = rayhitMinusZ;
      // sensorTarget.transform.position = rayhit.point;
    }

  }
}

