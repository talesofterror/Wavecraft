using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag("GuyBase")) {
			print(transform.name + ": Target hit.");
		}
	}
}
