using UnityEngine;
using UnityEditor;

public class HELPER_ObjectLinks : MonoBehaviour
{

  Vector3 xUpperBound;
  Vector3 xLowerBound;
  Vector3 yUpperBound;
  Vector3 yLowerBound;
  public GameObject[] boundsArray = new GameObject[4];

  void Awake()
  {

  }

  void OnDrawGizmosSelected()
  {
#if UNITY_EDITOR
    processBounds();
    for (int i = 0; i < boundsArray.Length; i++)
    {
      if (i == 3)
      {
        Gizmos.DrawLine(boundsArray[i].transform.position, boundsArray[0].transform.position);
        Handles.Label(boundsArray[i].transform.position, boundsArray[i].name);
        return;
      }
      Gizmos.color = Color.red;
      Gizmos.DrawLine(boundsArray[i].transform.position, boundsArray[i + 1].transform.position);
      Handles.Label(boundsArray[i].transform.position, boundsArray[i].name);
    }
#endif
  }

  private void processBounds()
  {
    for (int i = 0; i < boundsArray.Length; i++)
    {
      if (boundsArray[i].name == "XUpperBound")
      {
        xUpperBound = boundsArray[i].transform.position;
      }
      else if (boundsArray[i].name == "XLowerBound")
      {
        xLowerBound = boundsArray[i].transform.position;
      }
      else if (boundsArray[i].name == "YUpperBound")
      {
        yUpperBound = boundsArray[i].transform.position;
      }
      else if (boundsArray[i].name == "XLowerBound")
      {
        yLowerBound = boundsArray[i].transform.position;
      }
    }
  }

}
