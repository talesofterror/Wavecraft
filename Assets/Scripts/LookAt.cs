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
    if (targetMode == TargetMode.publicObject)
    {
      
    }
    if (targetMode == TargetMode.cursorControl)
    {
      sensorScript = sensor.GetComponent<PointerSensorManager>();
    }
  }

  void Update()
  {
    if (targetMode == TargetMode.publicObject)
    {
      targetTransform = publicTarget.transform.position;
      transform.LookAt(targetTransform);
    }
    if (targetMode == TargetMode.cursorControl)
    {
      transform.LookAt(sensorScript.cursorTarget.transform.position);
    }
  }
}
