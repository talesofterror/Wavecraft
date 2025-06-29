using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
  Collider _collider;
  EnemyStats enemyStats;
  Vector3 shrinkSize;
  bool falling;
  public float shrinkFactor = 0.5f;
  Vector3 fullScale;
  NavMeshAttack navMeshAttack;

  [HideInInspector] public bool dead = false;

  public bool disableColliderOnDeath = false;

  public enum DeathSequence
  {
    ShrinkAndGrow,
    Fall,
    Explode,
    Freeze,

  }
  public DeathSequence deathSequence = new DeathSequence();

  void Awake()
  {
    _collider = GetComponent<Collider>();
    enemyStats = GetComponent<EnemyStats>();
    if (GetComponent<NavMeshAttack>())
    {
      navMeshAttack = GetComponent<NavMeshAttack>();
    }
    shrinkState = ShrinkState.normal;
    shrinkSize = transform.localScale * shrinkFactor;
    fullScale = transform.localScale;
  }

  void Update()
  {

    if (enemyStats.hP <= 0)
    {
      dead = true;
    }
    else
    {
      dead = false;
    }

    if (dead == true)
    {
      if (disableColliderOnDeath)
      {
        _collider.enabled = false;
      }
      enemyStats.hP = 0;
      if (navMeshAttack)
      {
        navMeshAttack.NavMeshAgent.speed = 0;
      }
      StartCoroutine(hpZero());
    }
    if (dead == false)
    {
      _collider.enabled = true;
      if (navMeshAttack)
      {
        navMeshAttack.NavMeshAgent.speed = 3.5f;
      }
      StopCoroutine(hpZero());
    }
  }

  // ~ hpZero ()
  IEnumerator hpZero()
  {
    if (dead == true)
    {
      print(transform.name + " died!");

      // ~ DEATH ANIMATIONS

      // ! Shrink and Grow

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

      // ! Fall 

      if (deathSequence == DeathSequence.Fall)
      {
        StartCoroutine(fallIE());
      }

      // ! Explode 

      if (deathSequence == DeathSequence.Explode)
      {

      }

      // ! Freeze 

      if (deathSequence == DeathSequence.Freeze)
      {

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
    if (shrinkState != ShrinkState.growing)
    {
      shrinkState = ShrinkState.shrinking;
      shrinkLerpState += 2f * Time.deltaTime;
      transform.localScale = Vector3.Lerp(fullScale, shrinkSize, shrinkLerpState);
      if (shrinkLerpState > 1f)
      {
        shrinkState = ShrinkState.shrunk;
        yield return new WaitForSeconds(3);
        shrinkLerpState = 0;
        shrinkState = ShrinkState.growing;
      }
      yield return null;
    }
    if (shrinkState == ShrinkState.growing)
    {
      shrinkLerpState += 2f * Time.deltaTime;
      transform.localScale = Vector3.Lerp(shrinkSize, fullScale, shrinkLerpState);
      dead = false;
      if (shrinkLerpState > 1f)
      {
        shrinkState = ShrinkState.normal;
        shrinkLerpState = 0;
        enemyStats.hP = enemyStats.baseHP;
      }
      yield return null;
    }
  }

  IEnumerator fallIE()
  {
    yield return null;
  }
}

