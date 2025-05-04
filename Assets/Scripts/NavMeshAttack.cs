using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAttack : MonoBehaviour
{
  Vector3 defaultPosition;
  Vector3 defaultRotation;
  GameObject player;
  [HideInInspector]
  public NavMeshAgent NavMeshAgent;
  EnemyDamage enemyDamage;
  [HideInInspector]
  public Vector3 destination;

  public float sightThreshold;
  public float forgetThreshold;

  enum SenseStatus
  {
    sighted,
    unsighted
  }

  SenseStatus senseStatus = SenseStatus.unsighted;


  // & convert to collisions based system
  
  // & player position currently points to an area well below the mesh and 
  // & needs to not. 

  void Start()
  {
    defaultPosition = transform.position;
    defaultRotation = transform.eulerAngles;

    player = PLAYERSingleton.i.gameObject;
    NavMeshAgent = GetComponent<NavMeshAgent>();
    enemyDamage = GetComponent<EnemyDamage>();
  }

  public bool playerSensed(Vector3 playerPosition, Vector3 selfPosition, float threshhold)
  {
    float currentDistance = Vector3.Distance(selfPosition, playerPosition);
    if (currentDistance < threshhold) return true;
    else return false;
  }


  void Update()
  {
    NavMeshAgent.destination = destination;
    transform.eulerAngles = defaultRotation;

    if (playerSensed(player.transform.position, transform.position, sightThreshold))
    {
      senseStatus = SenseStatus.sighted;
      print("player within sight threshhold");
    }

    if (senseStatus == SenseStatus.sighted)
    {
      StartCoroutine(triggerFollow());
    }
    else
    {
      destination = defaultPosition;
      StopCoroutine(triggerFollow());
    }
  }

  float timer = 0.0f;
  public float sensedTimer = 2;
  IEnumerator triggerFollow()
  {
    print("trigger follow coroutine");
    if (timer < sensedTimer)
    {
      destination = player.transform.position;
      timer += 1f * Time.deltaTime;
      yield return null;
    }
    if (timer >= sensedTimer)
    {
      timer = 0;
      senseStatus = SenseStatus.unsighted;
      StopCoroutine(triggerFollow());
    }
  }


  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, sightThreshold);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, forgetThreshold);

  }
}
