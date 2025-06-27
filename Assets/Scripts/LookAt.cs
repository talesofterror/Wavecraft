using UnityEngine;

public class LookAt : MonoBehaviour
{

  public GameObject publicTarget;
  PointerSensorManager sensorScript;
  [SerializeField] float zOffset;

  public enum TargetMode
  {
    publicObject,
    player,
    cursorControl
  }

  public TargetMode targetMode = new TargetMode();

  void Start()
  {
    if (targetMode == TargetMode.cursorControl)
    {
      // sensorScript = sensor.GetComponent<PointerSensorManager>();
      sensorScript = CAMERASingleton.i.pointerSensor;
    }
  }

  void Update()
  {
    Vector3 offset = new Vector3(0, 0, zOffset);

    if (targetMode == TargetMode.publicObject)
    {
      transform.LookAt(publicTarget.transform.position + offset);
    }
    if (targetMode == TargetMode.cursorControl)
    {
      transform.LookAt(sensorScript.cursorGameObject.transform.position + offset);
    }
    if (targetMode == TargetMode.player)
    {
      transform.LookAt(PLAYERSingleton.i.transform.position + offset);
    }
  }
}
