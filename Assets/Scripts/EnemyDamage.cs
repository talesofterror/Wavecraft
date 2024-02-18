using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
  Collider _collider;
  EnemyStats enemyStats;
  Vector3 shrinkSize;
  Vector3 initScale;
  NavMeshAttack navMeshAttack;

  bool dead = false;

  public enum DeathSequence
  {
    ShrinkAndGrow
  }
  public DeathSequence deathSequence = new DeathSequence();

  void Awake()
  {
    _collider = GetComponent<Collider>();
    enemyStats = GetComponent<EnemyStats>();
    if (GetComponent<NavMeshAttack>()){
      navMeshAttack = GetComponent<NavMeshAttack>();
    }
    shrinkState = ShrinkState.normal;
    shrinkSize = transform.localScale * 0.5f;
    initScale = transform.localScale;
  }

  void Update()
  {

    if (enemyStats.hP <= 0)
    {
      dead = true;
      enemyStats.hP = 0;
      StartCoroutine(hpZero());
    }
    else
    {
      dead = false;
      StopCoroutine(hpZero());
    }

    if (dead == true) {
      _collider.enabled = false;
    }
    if (dead == false) {
      _collider.enabled = true;
    }
  }

  IEnumerator hpZero()
  {
    if (dead == true)
    {
      print("hpZero Called");

      // ? DEATH ANIMATIONS

      // ? Shrink and Grow

      if (deathSequence == DeathSequence.ShrinkAndGrow)
      {
        if (shrinkState == ShrinkState.normal | shrinkState == ShrinkState.shrinking)
        {
          StartCoroutine(shrinkAndGrowIE());
        }
        if (shrinkState == ShrinkState.growing)
        {
          shrinkState = ShrinkState.growing;
          StartCoroutine(shrinkAndGrowIE());
        }
      }
    }

    if (dead == false)
    {
      StopAllCoroutines();
    }

    yield return null;

  }

  private enum ShrinkState
  {
    normal,
    shrinking,
    shrunk,
    growing
  }
  private ShrinkState shrinkState = new ShrinkState();

  float shrinkLerpState = 0;
  IEnumerator shrinkAndGrowIE()
  {
    print("shrinkAndGrowIE called");
    if (shrinkState == ShrinkState.normal | shrinkState == ShrinkState.shrinking | shrinkState == ShrinkState.shrunk)
    {
      shrinkState = ShrinkState.shrinking;
      shrinkLerpState += 2f * Time.deltaTime;
      transform.localScale = Vector3.Lerp(initScale, shrinkSize, shrinkLerpState);
      if (shrinkLerpState > 1f)
      {
        shrinkState = ShrinkState.shrunk;
        yield return new WaitForSeconds(3);
        shrinkLerpState = 0;
        shrinkState = ShrinkState.growing;
        // StopCoroutine(shrinkAndGrowIE());
      }
      yield return null;
    }
    if (shrinkState == ShrinkState.growing)
    {
      shrinkLerpState += 2f * Time.deltaTime;
      transform.localScale = Vector3.Lerp(shrinkSize, initScale, shrinkLerpState);
      if (shrinkLerpState > 1f)
      {
        shrinkState = ShrinkState.normal;
        shrinkLerpState = 0;
        enemyStats.hP = enemyStats.baseHP;
        dead = false;
      }
      yield return null;
    }
  }
}
