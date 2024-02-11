using UnityEngine;

public class Overlayer : MonoBehaviour
{
  private Camera cam;
  public GameObject mainCamObject;
  private viewer mainCamViewer;
  private ViewState currentState;

  private bool swivel = true;

  // Start is called before the first frame update
  void Start()
  {
    cam = GetComponent<Camera>();
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
      if (Input.GetKeyDown(KeyCode.C)) {
        if (swivel == true){
          swivel = false;
        } else {
          swivel = true;
        }
      }
      if (swivel == true) {
        cam.orthographic = true;
      }
      if (swivel == false) {
        cam.orthographic = false;
        cam.fieldOfView = 44f;
      }
    }
  }
}
