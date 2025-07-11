using UnityEngine;

public class AreaDefiner : MonoBehaviour
{

  public ViewComponent viewComponent;

  [SerializeField] GameObject[] boundVertices;

  AreaBoundsObject bounds;

  void Awake()
  {
    bounds = new AreaBoundsObject(boundVertices);
  }

  public bool PlayerIsWithinBounds()
  {
    if (PLAYERSingleton.i.transform.position.x > this.bounds.xLowerBound
      && PLAYERSingleton.i.transform.position.y > this.bounds.yLowerBound
      && PLAYERSingleton.i.transform.position.x < this.bounds.xUpperBound
      && PLAYERSingleton.i.transform.position.y < this.bounds.yUpperBound)
    {
      return true;
    }
    else { return false; }
  }

}
