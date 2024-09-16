using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSpawner : MonoBehaviour
{

  public GameObject projectile;
  private GameObject[] projectilePool;
  public int projectilePoolSize = 10;

  void Start()
  {
    projectilePool = new GameObject[projectilePoolSize];
    for (int i = 0; i < projectilePool.Length; i++) {
      projectilePool[i] = GameObject.Instantiate(projectile, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
    }
  }

  void Update()
  {

  }
}
