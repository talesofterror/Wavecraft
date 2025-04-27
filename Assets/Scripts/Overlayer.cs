using System;
using UnityEngine;

public class Overlayer : MonoBehaviour
{
  private Camera overlayCam;
  public GameObject mainCamObject;
  private Camera mainCamCamera;
  private View mainCamViewer;
  private ViewState currentState;
  public float overlayFOV = 44f; // & 44f

  private bool swivel = false;

  // Start is called before the first frame update
  void Start()
  {
    overlayCam = gameObject.GetComponent<Camera>();
    mainCamViewer = mainCamObject.GetComponent<View>();
    mainCamCamera = mainCamObject.GetComponent<Camera>();
  }

  // Update is called once per frame
  void Update()
  {

    overlayCam.transform.position = mainCamObject.transform.position;
    overlayCam.transform.rotation = mainCamObject.transform.rotation;
    overlayCam.fieldOfView = mainCamCamera.fieldOfView;

  }
}
