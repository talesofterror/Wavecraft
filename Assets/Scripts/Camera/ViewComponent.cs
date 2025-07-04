using UnityEngine;

public class ViewComponent : MonoBehaviour
{
  public ViewerObject view;

  void Start()
  {
    Vector3 defaultView = new Vector3(transform.position.x, transform.position.y, -26);
    if (view.position == Vector3.zero)
    {
      view.position = defaultView;
      view.fieldOfView = 38;
      view.followState = FollowState.Total;
    }
  }

}
