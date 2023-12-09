using UnityEngine;

public class LookAt : MonoBehaviour
{
  
  public GameObject publicTarget;
  public GameObject cam;
  viewer camScript;

  public enum TargetMode {
    publicObject, 
    playerControl
  }

  public TargetMode targetMode = new TargetMode();

  Vector3 targetTransform;

    // Start is called before the first frame update
    void Start()
    {
      camScript = this.cam.GetComponent<viewer>();
    }

    // Update is called once per frame
    void Update()
    {
      if (targetMode == TargetMode.publicObject){
        // targetTransform = publicTarget.transform.position;
        transform.LookAt(targetTransform);
      }
      if (targetMode == TargetMode.playerControl){
        transform.eulerAngles = new Vector3(0, 0, 0);
        Vector3 rayhitMinusZ = new Vector3(
          camScript.debugSphere.transform.position.x,
          camScript.debugSphere.transform.position.y,
          0 );
        targetTransform = camScript.rayhit.point;
        transform.LookAt(rayhitMinusZ);
      }
    }
}
