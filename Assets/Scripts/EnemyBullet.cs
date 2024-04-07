using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
[ExecuteAlways]
  public int bulletPoolSize;
  GameObject[] bulletPool;
  SphereCollider[] bulletColliders;
  MeshRenderer[] bulletRenderers;
  Rigidbody[] bulletRigidBodies;
  public Transform[] spawnPoints;

  public float detectionDistance;

  public enum ShootStyle
  {
    intermittent,
    nonstop,

  };

  ShootStyle shootStyle = ShootStyle.intermittent;

  void Start()
  {
    bulletPool = new GameObject[bulletPoolSize];
    for (int i = 0; i < bulletPoolSize; i++)
    {
      bulletPool[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      bulletPool[i].transform.position = transform.position * 2;
      bulletPool[i].AddComponent<Rigidbody>();
      bulletPool[i].name = "bullet " + i;
      print("bullet generated: #"+i);
      bulletColliders[i] = bulletPool[i].GetComponent<SphereCollider>();
      bulletRenderers[i] = bulletPool[i].GetComponent<MeshRenderer>();
      bulletRigidBodies[i] = bulletPool[i].GetComponent<Rigidbody>();
    }
  }

  void Update()
  {
    if (shootStyle == ShootStyle.intermittent)
    {
      StartCoroutine(shootIntermittentIE());
    }
  }

  IEnumerator shootIntermittentIE()
  {

    yield return null;
  }
}
