using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
  bool knockedBack = false;


  void OnCollisionEnter(Collision collission)
  {
    if (collission.gameObject.tag == "PlayerDamage")
    {
      Vector3 contactPoint = collission.GetContact(0).point;
      Vector3 collisionVector = collission.gameObject.transform.position;
      if (!knockedBack)
      {
        StartCoroutine(Knockback(contactPoint, collisionVector));
      }
      if (knockedBack)
      {
        StopAllCoroutines();
        StartCoroutine(Knockback(contactPoint, collisionVector));
      }

      // ? get direction of collision
      // subtract collision.contact from collision.gameObject.transform 
      // ? move with coroutine in direction



    };
  }

  public float knockbackDistance = 1.5f;
  public float knockbackSpeed = 1f;


  IEnumerator Knockback(Vector3 contactPoint, Vector3 collisionVector)
  {
    knockedBack = true;
    float i;
    Vector3 startPosition = transform.position;
    Vector3 knockbackDirection = contactPoint - collisionVector;
    Vector3 knockbackFactor = new Vector3(knockbackDirection.x * knockbackDistance, knockbackDirection.y * knockbackDistance, 0);
    Vector3 knockbackPositon = transform.position + knockbackFactor;
    for (i = 0; i <= 1; i += knockbackSpeed * Time.deltaTime)
    {
      transform.position = Vector3.Lerp(startPosition, knockbackPositon, i);
      print("knockback coroutine iteration: " + i);
      yield return null;
    }
    for (i = 1; i > 0; i -= knockbackSpeed * Time.deltaTime)
    {
      transform.position = Vector3.Lerp(startPosition, knockbackPositon, i);
      print("knockback coroutine iteration: " + i);
      yield return null;
    }
    knockedBack = false;
  }

  void Update()
  {

  }
}
