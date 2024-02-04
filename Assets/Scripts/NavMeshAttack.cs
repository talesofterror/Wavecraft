using UnityEngine;
using UnityEngine.AI;

public class NavMeshAttack : MonoBehaviour
{

  GameObject player;
  NavMeshAgent NavMeshAgent;

  public float distanceThreshold;


  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    NavMeshAgent = GetComponent<NavMeshAgent>();
  }

  public bool playerSighted(Vector3 playerPosition, Vector3 selfPosition, float threshhold)
  {
    float currentDistance = Vector3.Distance(selfPosition, playerPosition);
    if (currentDistance < threshhold) return true;
    else return false;
  }

  void Update()
  {
    if (playerSighted(player.transform.position, transform.position, distanceThreshold))
    {
      Vector3 destination = player.transform.position;
      NavMeshAgent.destination = destination;
    };
  }

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, distanceThreshold);
  }

}
