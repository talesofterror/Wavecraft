using UnityEngine;
using UnityEditor;


public class AreaBoundsObject
{

  public float xUpperBound;
  public float xLowerBound;
  public float yUpperBound;
  public float yLowerBound;

  [HideInInspector] public GameObject[] array = new GameObject[4];

  public AreaBoundsObject(GameObject[] bounds)
  {
    processBounds(bounds);
  }


  private void processBounds(GameObject[] array)
  {
    for (int i = 0; i < array.Length; i++) 
    {
      if (array[i].name == "XUpperBound")
      {
        xUpperBound = array[i].transform.position.x;
      }
      else if (array[i].name == "XLowerBound")
      {
        xLowerBound = array[i].transform.position.x;
      }
      else if (array[i].name == "YUpperBound")
      {
        yUpperBound = array[i].transform.position.y;
      }
      else if (array[i].name == "YLowerBound")
      {
        yLowerBound = array[i].transform.position.y;
      }
    }

  }
  
}
