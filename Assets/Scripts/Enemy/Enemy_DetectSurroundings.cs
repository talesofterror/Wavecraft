using UnityEngine;
using UnityEditor;

public class Enemy_DetectSurroundings : MonoBehaviour
{
	public Collider _collider;
	public GameObject targetGameObject;


	public bool detection = false;

	
	void Start()
	{
		_collider = gameObject.GetComponent<Collider>();
	}

	void Update()
	{
			
	}

	void OnTriggerEnter (Collider collider) {
		targetGameObject = collider.gameObject;
		detection = true; 
	}

  	void OnTriggerExit (Collider collider) {
		detection = false;
	}

  void OnDrawGizmos()
  {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<SphereCollider>().radius);
		Handles.Label(transform.position, transform.name);
  }

}

