using Unity.Mathematics;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

  public MeshRenderer _renderer;
  Collider _collider;
  bool targetHit;
  public GameObject particleObject;

  Enemy_ProjectileGun sourceGun;
  
  void Start()
  {
    // _renderer = GetComponentInChildren<MeshRenderer>();
    _collider = GetComponent<BoxCollider>();
    sourceGun = GetComponentInParent<Enemy_ProjectileGun>();
    _renderer.enabled = false;
  }

  void Update()
  {
    
  }

  void OnTriggerEnter (Collider collider) {

    if (collider.CompareTag("Data")) {
      return;
    }

    GameObject splash = Instantiate(particleObject, transform.position, quaternion.identity);
    // splash.transform.parent = null;
    gameObject.SetActive(false);
    _collider.enabled = false;
    
		if (collider.gameObject.CompareTag("GuyBase")) {
      print(transform.name + " hit the Player.");
      PLAYERSingleton.i.takeDamage(sourceGun.projectileDamage);
      PLAYERSingleton.i.rB.linearVelocity = Vector3.zero;
      PLAYERSingleton.i.rB.AddForce(gameObject.GetComponent<Rigidbody>().linearVelocity * 50);
		}
    if (collider.CompareTag("PlayerDamage")) {
      print(sourceGun.gameObject.transform.parent.name + " hit the player projectile.");
      // gameObject.SetActive(false);
    }
    else {
      return;
    }
	}

  void CollisionEnter(Collision collision)
  {
    print(transform.name + " collided");

    if (collision.gameObject.tag == "GuyBase") {
    }
  }
}
