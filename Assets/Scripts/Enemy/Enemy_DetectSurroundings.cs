using UnityEngine;
using UnityEditor;

public class Enemy_DetectSurroundings : MonoBehaviour
{
  [SerializeField] GameObject targetGameObject;
  GameObject player;
  [HideInInspector] public GameObject target;


  public bool detection = false;


  void Start()
  {
    if (targetGameObject)
    {
      target = targetGameObject;
    }
    else
    {
      target = PLAYERSingleton.i.gameObject;
    }
  }

  void Update()
  {

  }

  void OnTriggerEnter(Collider collider)
  {
    if (PLAYERSingleton.i.areaTransition)
    {
      return;
    }
    if (collider.gameObject == PLAYERSingleton.i.gameObject)
    {
      target = collider.gameObject;
      detection = true;
    }
  }

  void OnTriggerExit(Collider collider)
  {
    detection = false;
  }

  void OnDrawGizmos()
  {
#if UnityEditor
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<SphereCollider>().radius);
		Handles.Label(transform.position, transform.name);
#endif
  }
}

