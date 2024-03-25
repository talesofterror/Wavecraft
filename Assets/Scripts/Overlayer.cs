using UnityEngine;

public class Overlayer : MonoBehaviour
{
  private Camera cam;
  public GameObject mainCamObject;
  private Transform mainCamDefaulPosition;

  private viewer mainCamViewer;
  private ViewState currentState;

  private bool swivel = true;

  // Start is called before the first frame update
  void Start()
  {
    cam = GetComponent<Camera>();
    mainCamViewer = mainCamObject.GetComponent<viewer>();
    mainCamDefaulPosition = mainCamObject.transform;
  }

  // Update is called once per frame
  void Update()
  {
    currentState = mainCamViewer.state;

    if (currentState == ViewState.stageView)
    {
      cam.orthographic = false;
      cam.fieldOfView = 28f;
    }
    else
    {
      if (mainCamViewer.swivelOn == true) {
        cam.orthographic = true;
      }
      if (mainCamViewer.swivelOn == false) {
        cam.orthographic = false;
        cam.fieldOfView = 44f;
      }
    }
  }
}
