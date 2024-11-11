using UnityEngine;
using UnityEditor;

public class Enemy_DetectSurroundings : MonoBehaviour
{
	public Collider _collider;
	public GameObject targetGameObject;


	public Detection detection = new Detection();

	
	void Start()
	{
		detection = Detection.Dormant;
		_collider = gameObject.GetComponent<Collider>();
	}

	void Update()
	{
			
	}

	void OnTriggerEnter (Collider collider) {
		print(transform.name + "detected something!");
		targetGameObject = collider.gameObject;
		detection = Detection.Active; 
	}

  void OnDrawGizmos()
  {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<SphereCollider>().radius);
		Handles.Label(transform.position, transform.name);
  }

}

public enum Detection {
	Active, 
	Dormant
}
