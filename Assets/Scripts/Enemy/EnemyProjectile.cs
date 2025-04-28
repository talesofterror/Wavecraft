using UnityEditor.Rendering;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

  MeshRenderer renderer;
  Collider _collider;

  void Start()
  {
    renderer = GetComponent<MeshRenderer>();
    _collider = GetComponent<BoxCollider>();
  }

  void OnTriggerEnter (Collider collider) {

    print(transform.name + " triggered");
    renderer.enabled = false;
    _collider.enabled = false;

		if (collider.gameObject.CompareTag("GuyBase")) {
      print(transform.name + " hit the Player.");
		}
    if (collider.CompareTag("PlayerDamage")) {
      print(transform.name + " hit the player projectile.");
      // gameObject.SetActive(false);
    }
	}

  void CollisionEnter(Collision collision)
  {
    print(transform.name + " collided");

    if (collision.gameObject.tag == "GuyBase") {
    }
  }
}
