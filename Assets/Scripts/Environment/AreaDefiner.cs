using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Rendering;

public class AreaDefiner : MonoBehaviour
{

  public ViewComponent viewComponent; 

  [SerializeField] GameObject[] boundVertices;

  AreaBoundsObject bounds;

  void Start()
  {
    bounds = new AreaBoundsObject(boundVertices);
  }

  public bool PlayerIsWithinBounds()
  {
    bool yes = PLAYERSingleton.i.transform.position.x < this.bounds.xLowerBound
            && PLAYERSingleton.i.transform.position.x < this.bounds.yLowerBound
            && PLAYERSingleton.i.transform.position.x < this.bounds.xUpperBound
            && PLAYERSingleton.i.transform.position.x < this.bounds.yUpperBound;

    if (yes)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

}
