using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SporeManager : MonoBehaviour
{
  EnemyStats eStats;
  private float damagerDamage;
  public Vector3 shrinkSize;
  private Vector3 fullScale;
  float shrinkSizeValue = 0.01f;

  public float lerpSmallTime = 1f;
  // Start is called before the first frame update
  void Start()
  {
    eStats = GetComponent<EnemyStats>();
    fullScale = transform.localScale;
    shrinkSize = new Vector3(shrinkSizeValue, shrinkSizeValue, shrinkSizeValue);
  }

  void OnCollisionEnter()
  {
    damagerDamage = GetComponent<AmmoCollisions>().damage;
    eStats.enemyHP -= damagerDamage;
  }

  // Update is called once per frame
  void Update()
  {

    if (eStats.enemyHP <= 0)
    {
      hpZero();
    }
  }

  void hpZero()
  {
    transform.localScale -= shrinkSize;
    print("hpZero Called");
    StartCoroutine(shrinkIE(transform.localScale, shrinkSize));
  }

  IEnumerator shrinkIE(Vector3 targetScale, Vector3 startingScale)
  {

    if (lerpSmallTime >= 1)
    {
      lerpSmallTime -= 0.1f; 
      // colliderToggle.enabled = false;
      //print("Lerp Shrink Timer = " + lerpSmallTime);
      yield return new WaitForSeconds(1f);
    }
    if (lerpSmallTime <= 0.2f)
    {
      lerpSmallTime = 0.2f;
      transform.localScale = targetScale;
      // isShrinking = false;
      // isShrunk = true;
    }
  }
}
