using UnityEditorInternal;
using UnityEngine;

public class UTILITY
{

  public static void SetToggleRendererColider(GameObject gameObject, string command = null)
  {
    MeshRenderer _renderer = gameObject.GetComponentInChildren<MeshRenderer>();
    Collider _collider = gameObject.GetComponentInChildren<Collider>();

    if (command != null)
    {
      if (command == "on")
      {
        _collider.enabled = true;
        _renderer.enabled = true;
      }
      if (command == "off")
      {
        _collider.enabled = false;
        _renderer.enabled = false;
      }
    }

    _collider.enabled = !_collider.enabled;
    _renderer.enabled = !_renderer.enabled;
  }

  public static Vector3 getDirectionVector3(Vector3 start, Vector3 target)
  {
    Vector3 heading = target - start;
    float distance = heading.magnitude;
    Vector3 direction = heading / distance;
    return direction;
  }

  // public static GameObject[] getChildrenAsArray (params GameObject[] children)
  // {
  //   GameObject[] childArray = new GameObject[children.Length];

  //   for (int i = 0; i < children.Length; i++) {
  //     childArray[i] = 
  //   }

  //   return new GameObject[0];
  // }
}
