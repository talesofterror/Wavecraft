using UnityEngine;

public class PlayerEnemyInteractions : MonoBehaviour
{
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Enemy"))
    {
      Debug.Log("Enemy collision detected.");
      float damageAmount = collision.gameObject.GetComponent<EnemyStats>().aP;
      PLAYERSingleton.i.takeDamage(damageAmount, collision.gameObject);
    }
  }
}
