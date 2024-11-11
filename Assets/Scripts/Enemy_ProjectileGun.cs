using UnityEngine;
using System.Collections;

public class Enemy_ProjectileGun : MonoBehaviour
{
	
	EnemyProjectileSpawner bulletSpawner;
	Enemy_DetectSurroundings detector;
	public GameObject bullet;
	public Transform targetTransform;

	// Start is called before the first frame update
	void Start()
	{
			bulletSpawner = new EnemyProjectileSpawner(bullet, gameObject, 10);
			detector = gameObject.GetComponentInChildren<Enemy_DetectSurroundings>();
	}

	// Update is called once per frame
	void Update()
	{
		if (detector.detection == Detection.Active)	{
			bulletSpawner.target = detector.targetGameObject.transform;
			print("bullet spawner target position: " + bulletSpawner.target.position);
			StartCoroutine(FireAllWaitSeconds(1)); 
			detector.detection = Detection.Dormant;
		}
	}

	public IEnumerator FireAllWaitSeconds (float seconds) {
		for (int i = 0; i < bulletSpawner.projectilePool.Length; i++) {
			bulletSpawner.projectilePool[i].transform.position = transform.position;
			bulletSpawner.projectilePool[i].GetComponent<Collider>().enabled = true;
			bulletSpawner.projectilePool[i].GetComponent<Renderer>().enabled = true;
			bulletSpawner.projectilePool[i].GetComponent<Rigidbody>().velocity = calculateVelocity(bulletSpawner.target, 5f);
			print(calculateVelocity(bulletSpawner.target, 5f));
			yield return new WaitForSeconds(seconds);
			if (i == bulletSpawner.projectilePool.Length+1) {
				StopCoroutine(FireAllWaitSeconds(0));
			}
		}
	}

	Vector3 calculateVelocity (Transform _target, float _speed) {
		Vector3 heading = _target.position - gameObject.transform.position;
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;
		return direction * _speed;
	}
}
