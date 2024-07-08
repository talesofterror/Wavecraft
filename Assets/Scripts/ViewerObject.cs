using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerObject
{
  public Transform transform;

  public float fOV;

  public ViewerObject (Transform t) {
    transform = t;
  }

  // Start is called before the first frame update
  public void UpdateViewer(
    float xOffset, float yOffset, float zOffset,
    float xRotation, float yRotation, float zRotation,
    float fOV
                          )
  {

  }

}
