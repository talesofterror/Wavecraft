using UnityEngine;

public class PointerSensorManager : MonoBehaviour
{
  private Camera cam;
  LayerMask sensorLayer;
  public RaycastHit rayhit;
  public GameObject cursorGameObject; 
  GameObject player;

  void Awake()
  {
    // Debug.Log(GameObject.FindWithTag("GuyBase").name);
    // Debug.Log(PLAYERSingleton.i.transform.position);
    cam = Camera.main;
    cursorGameObject.name = "****** Cursor!";
    cursorGameObject.layer = 5;

    sensorLayer = 1 << 6;
  }

  void Start()
  {
    player = PLAYERSingleton.i.gameObject;
  }

  Vector3 sensorPosition;
  void Update()
  {
    sensorPosition = new Vector3(
      Camera.main.gameObject.transform.position.x,
      Camera.main.gameObject.transform.position.y,
      player.transform.position.z
      );

    transform.position = sensorPosition;
    
    newManager();
    // OldManager();
  }

  private void newManager()
  {
    Vector3 mousePosPlusDepth = new Vector3(Input.mousePosition.x, Input.mousePosition.y, player.transform.position.z);
    Vector3 screenPoint = cam.ScreenToWorldPoint(mousePosPlusDepth);
    Vector3 screenPointHeading = screenPoint - cam.gameObject.transform.position;
    float screenPointDistance = screenPointHeading.magnitude;
    Vector3 screenPointDirection = screenPointHeading / screenPointDistance;

    RaycastHit rayhit;


    if (Physics.Raycast(cam.gameObject.transform.position, screenPointDirection * 100, out rayhit, Mathf.Infinity, sensorLayer))
    {
      Debug.DrawRay(cam.gameObject.transform.position, screenPointDirection * 100, Color.red);

      Vector3 rayhitMinusZ = new Vector3(rayhit.point.x, rayhit.point.y, rayhit.point.z);

      cursorGameObject.transform.position = rayhitMinusZ;
    }
  }

  private void OldManager()
  {
    Ray rayForSensor = cam.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(rayForSensor.origin, rayForSensor.direction * 100, out rayhit, Mathf.Infinity, sensorLayer))
    {
      Debug.DrawRay(rayForSensor.origin, rayForSensor.direction * 100, Color.red);

      Vector3 rayhitMinusZ = new Vector3(rayhit.point.x, rayhit.point.y, player.transform.position.z);

      cursorGameObject.transform.position = rayhitMinusZ;
    }
  }
}

