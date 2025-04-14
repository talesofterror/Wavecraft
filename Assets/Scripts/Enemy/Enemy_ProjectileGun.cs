using UnityEngine;
using System.Collections;
using System.Diagnostics.Tracing;

public class Enemy_ProjectileGun : MonoBehaviour
{
	
	EnemyProjectileSpawner bulletSpawner;
	Enemy_DetectSurroundings detector;
	public GameObject bullet;
	[HideInInspector] public Transform targetTransform;
	public float bulletSpeed = 2f;
  public bool firing = false;

  public EnemyDamage enemyDamage;

	void Start()
	{
			bulletSpawner = new EnemyProjectileSpawner(bullet, gameObject, 10);
			detector = gameObject.GetComponentInChildren<Enemy_DetectSurroundings>();
	}

	// Update is called once per frame
	void Update()
	{
		if (detector.detection && !enemyDamage.dead)	{
      bulletSpawner.target = detector.targetGameObject.transform;
      if(!firing) {
        firing = true;
        StartCoroutine(FireAllWaitSeconds(1));
      }
		}
    if (enemyDamage.dead) {
      StopAllCoroutines();
      firing = false;
    }
	}

	public IEnumerator FireAllWaitSeconds (float seconds) {
		for (int i = 0; i < bulletSpawner.projectilePool.Length; i++) {
			if (i + 1 == bulletSpawner.projectilePool.Length) {
        firing = false;
				StopCoroutine(FireAllWaitSeconds(0));
			}
      bulletSpawner.projectilePool[i].gameObject.SetActive(true);
			bulletSpawner.projectilePool[i].transform.position = transform.position;
			bulletSpawner.projectilePool[i].GetComponent<Collider>().enabled = true;
			bulletSpawner.projectilePool[i].GetComponent<Renderer>().enabled = true;
			bulletSpawner.projectilePool[i].GetComponent<Rigidbody>().linearVelocity = calculateVelocity(bulletSpawner.target, bulletSpeed);
      bulletSpawner.projectilePool[i].transform.parent = null;
			yield return new WaitForSeconds(seconds);
		}
	}

	Vector3 calculateVelocity (Transform _target, float _speed) {
		Vector3 heading = _target.position - gameObject.transform.position;
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;
		return direction * _speed;
	}
}
