using UnityEditor.Rendering;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

  MeshRenderer _renderer;
  Collider _collider;
  bool targetHit;

  void Start()
  {
    _renderer = GetComponent<MeshRenderer>();
    _collider = GetComponent<BoxCollider>();
  }

  void Update()
  {

  }

  void OnTriggerEnter (Collider collider) {

    // print(transform.name + " triggered");
    _renderer.enabled = false;
    _collider.enabled = false;

		if (collider.gameObject.CompareTag("GuyBase")) {
      print(transform.name + " hit the Player.");
      PLAYERSingleton.i.takeDamage();
      PLAYERSingleton.i.rB.linearVelocity = Vector3.zero;
      PLAYERSingleton.i.rB.AddForce(gameObject.GetComponent<Rigidbody>().linearVelocity * 50);
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
