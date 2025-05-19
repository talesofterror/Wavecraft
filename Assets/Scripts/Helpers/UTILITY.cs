using UnityEngine;

public class UTILITY
{

  public static void SetToggleRendererColider (GameObject gameObject, string command = null) {
    MeshRenderer _renderer = gameObject.GetComponentInChildren<MeshRenderer>();
    Collider _collider = gameObject.GetComponentInChildren<Collider>();

    if (command != null) {
      if (command == "on") {
        _collider.enabled = true;
        _renderer.enabled = true;
      }
      if (command == "off") {
        _collider.enabled = false;
        _renderer.enabled = false;
      }
    }
    
    _collider.enabled = !_collider.enabled;
    _renderer.enabled = !_renderer.enabled;
  }
}
