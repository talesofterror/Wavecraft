using UnityEngine;

public class Overlayer : MonoBehaviour
{
  private Camera cam;
  public GameObject mainCam;
  private ViewerOld mainCamViewer;
  private ViewState currentState;
  public float overlayFOV = 44f; // & 44f

  private bool swivel = false;

  // Start is called before the first frame update
  void Start()
  {
    cam = GetComponent<Camera>();
    mainCam = Camera.main.gameObject;
    mainCamViewer = mainCam.GetComponent<ViewerOld>();
  }

  // Update is called once per frame
  void Update()
  {
    currentState = mainCamViewer.state;

    // cam.orthographic = false;
    cam.fieldOfView = overlayFOV;

    if (currentState == ViewState.stageView)
    {
      cam.orthographic = false;
      overlayFOV = 28f;
    }
    else
    {
        overlayFOV = 44f;
    }
  }
}
