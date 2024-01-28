using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Overlayer : MonoBehaviour
{
  private Camera cam;
  public GameObject mainCamObject;
  private viewer mainCamViewer;
  private ViewState currentState;
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
    }
    else
    {
      cam.orthographic = true;
    }
  }
}
