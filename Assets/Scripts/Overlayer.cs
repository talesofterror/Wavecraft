using UnityEngine;

public class Overlayer : MonoBehaviour
{
  private Camera cam;
  public GameObject mainCamObject;
  private viewer mainCamViewer;
  private ViewState currentState;

  private bool swivel = false;

  // Start is called before the first frame update
  void Start()
  {
    cam = GetComponent<Camera>();
    mainCamObject = Camera.main.gameObject;
    mainCamViewer = mainCamObject.GetComponent<viewer>();
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
