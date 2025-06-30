using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKnockback : MonoBehaviour
{
  public float knockbackDistance = 1.5f;
  public float knockbackSpeed = 1f;
  public float knockbackForce = 10;
  bool knockedBack = false;

  Rigidbody rB;
  NavMeshAgent navMeshAgent;

  void Start()
  {
    rB = GetComponent<Rigidbody>();
    if (GetComponent<NavMeshAgent>())
    {
      navMeshAgent = GetComponent<NavMeshAgent>();
    }
  }

  void Update()
  {

  }
  
  void OnCollisionEnter(Collision collission)
  {
    if (knockedBack)
    {
      return;
    }
    if (collission.gameObject.tag == "PlayerDamage" && !knockedBack)
    {
      // Vector3 contactPoint = collission.GetContact(0).point;
      // Vector3 collisionVector = collission.gameObject.transform.position;
      // if (!knockedBack)
      // {
      //   StartCoroutine(Knockback(contactPoint, collisionVector));
      // }
      // if (knockedBack)
      // {
      //   StopAllCoroutines();
      //   StartCoroutine(Knockback(contactPoint, collisionVector));
      // }

      Vector3 knockbackDirection = UTILITY.getDirectionVector3(
        collission.gameObject.GetComponent<AmmoCollisions>().playerPositionWhenFired, transform.position);
      StartCoroutine(Knockback(knockbackDirection));
    }
  }

  IEnumerator Knockback(Vector3 direction)
  {
    knockedBack = true;
    navMeshAgent.isStopped = true;
    // rB.linearVelocity = Vector3.zero;
    rB.isKinematic = false;
    rB.AddForce(direction * knockbackForce);
    rB.detectCollisions = false;
    yield return new WaitForSeconds(0.5f);
    rB.detectCollisions = true;
    rB.isKinematic = true;
    navMeshAgent.isStopped = false;
    knockedBack = false;

  }


  // IEnumerator Knockback(Vector3 contactPoint, Vector3 collisionVector)
  // {
  //   knockedBack = true;
  //   float i;
  //   Vector3 startPosition = transform.position;
  //   Vector3 knockbackDirection = contactPoint - collisionVector;
  //   Vector3 knockbackFactor = new Vector3(knockbackDirection.x * knockbackDistance, knockbackDirection.y * knockbackDistance, 0);
  //   Vector3 knockbackPositon = transform.position + knockbackFactor;
  //   for (i = 0; i <= 1; i += knockbackSpeed * Time.deltaTime)
  //   {
  //     transform.position = Vector3.Lerp(startPosition, knockbackPositon, i);
  //     print("knockback coroutine iteration: " + i);
  //     yield return null;
  //   }
  //   for (i = 1; i > 0; i -= knockbackSpeed * Time.deltaTime)
  //   {
  //     transform.position = Vector3.Lerp(startPosition, knockbackPositon, i);
  //     print("knockback coroutine iteration: " + i);
  //     yield return null;
  //   }
  //   knockedBack = false;
  // }

}
