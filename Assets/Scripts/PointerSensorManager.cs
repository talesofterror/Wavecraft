using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PointerSensorManager : MonoBehaviour
{
  private Camera cam;
  LayerMask sensorLayer;
  public RaycastHit rayhit;
  public GameObject cursorTarget; // & remove instantiating code when mesh crafted
  // public GameObject sensorTarget;
  public Material cursorObjectMaterial;

  public GameObject player;

  void Awake()
  {
    cam = Camera.main;
    Cursor.visible = false;
    // Cursor.lockState = CursorLockMode.Confined;
    cursorTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    cursorTarget.name = "****** Cursor Target!";
    cursorTarget.GetComponent<Collider>().enabled = false;
    cursorTarget.GetComponent<Renderer>().material = cursorObjectMaterial;
    print("Cursor Object Material = " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
    cursorTarget.layer = 5;

    // sensorTarget = GameObject.CreatePrimitive(PrimitiveType.Cube);
    // sensorTarget.name = "****** Sensor Target!";
    // sensorTarget.GetComponent<Collider>().enabled = false;
    // sensorTarget.layer = 5;

    sensorLayer = 1 << 6;
  }

  void Update()
  {
    Ray rayForSensor = cam.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(rayForSensor.origin, rayForSensor.direction * 100, out rayhit, Mathf.Infinity, sensorLayer))
    {
      Debug.DrawRay(rayForSensor.origin, rayForSensor.direction * 100, Color.red);

      Vector3 rayhitMinusZ = new Vector3(rayhit.point.x, rayhit.point.y, player.transform.position.z);

      cursorTarget.transform.position = rayhitMinusZ;
    }

  }
}

