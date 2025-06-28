using System;
using UnityEditor;
using UnityEngine;

public class HELPER_HighlightParent : MonoBehaviour
{
  public enum Tool
  {
    LineChildToParent,
    LineThroughChildArray
  }

  public Tool onSelectDo = new Tool();

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnDrawGizmosSelected()
  {
    switch (onSelectDo)
    {
      case Tool.LineChildToParent:
        Vector3 parentLocation = transform.parent.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, parentLocation);
        Handles.Label(transform.position, transform.name);
        break;
      case Tool.LineThroughChildArray:

        break;
      default:

        break;
    }
  }
  void DrawLineArray(Vector3[] vectorArray)
  {
    
  }
}

