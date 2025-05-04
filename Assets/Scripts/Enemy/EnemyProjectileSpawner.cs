using UnityEngine;

public class EnemyProjectileSpawner
{

  public GameObject projectile;
  public GameObject[] projectilePool;
  public int poolSize = 10;
  public float projectileDamage;

	public Transform target;

	public enum State {
		Dormant,
		Active
	}
	
	public State spawnerState = new State();

  public EnemyProjectileSpawner(GameObject _projectile, GameObject parent, int poolSize) {
		projectile = _projectile;
    projectilePool = new GameObject[poolSize];
    for (int i = 0; i < projectilePool.Length; i++) {
      projectilePool[i] = GameObject.Instantiate(projectile, parent.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
			projectilePool[i].name = parent.transform.name + " Projectile #" + (i + 1);
			projectilePool[i].GetComponent<Collider>().enabled = false;
			projectilePool[i].GetComponentInChildren<Renderer>().enabled = false;
			projectilePool[i].transform.parent = parent.transform;
			spawnerState = State.Dormant;
    }
  }

}
