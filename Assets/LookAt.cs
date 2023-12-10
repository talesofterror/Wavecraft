using UnityEngine;

public class LookAt : MonoBehaviour
{

  public GameObject publicTarget;
  public GameObject sensor;
  PointerSensorManager sensorScript;

  public enum TargetMode
  {
    publicObject,
    cursorControl
  }

  public TargetMode targetMode = new TargetMode();

  Vector3 targetTransform;

  void Start()
  {
    sensorScript = sensor.GetComponent<PointerSensorManager>();
  }

  void Update()
  {
    if (targetMode == TargetMode.publicObject)
    {
      transform.LookAt(targetTransform);
    }
    if (targetMode == TargetMode.cursorControl)
    {

      transform.LookAt(sensorScript.cursorTarget.transform.position);
    }
  }
}
