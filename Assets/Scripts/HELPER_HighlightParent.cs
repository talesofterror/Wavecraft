using UnityEditor;
using UnityEngine;

public class HELPER_HighlightParent : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnDrawGizmosSelected()
  {
    Vector3 parentLocation = transform.parent.transform.position;
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, parentLocation);
    Handles.Label(transform.position, transform.name);
  }
}
