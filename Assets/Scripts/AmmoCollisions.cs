using UnityEngine;

public class AmmoCollisions : MonoBehaviour
{
  public GameObject dustBall;
  public EnemyStats enemyStats;
  public float damage = 10;
  public int dustAmount = 3;
  public float dustDuration = 0.5f;
  Rigidbody rB;
  Rigidbody rBDust;
  MeshRenderer mrenderer;
  Collider mcollider;

  void Awake()
  {
    mrenderer = GetComponent<MeshRenderer>();
    mcollider = GetComponent<Collider>();
    rB = this.gameObject.GetComponent<Rigidbody>();
  }

  void Update()
  {

  }

  private void OnCollisionEnter(Collision collision)
  {

    mrenderer.enabled = false;

    if (collision.gameObject.tag == "Enemy")
    {
      if (collision.gameObject.GetComponent<EnemyProjectile>())
      {
        collision.gameObject.SetActive(false);
      }
      enemyStats = collision.gameObject.GetComponent<EnemyStats>();
      enemyStats.hP -= damage;
    }

    mcollider.enabled = false;

    DustBloom(collision);
  }

  private void DustBloom(Collision collision)
  {
    for (int i = 1; i <= dustAmount; ++i)
    {
      // float dustLoc = Mathf.Sin(0.5f + i);
      // ammoLoc = transform.position + new Vector3(0.5f, dustLoc, 0f);
      GameObject dustObject = Instantiate(dustBall, collision.transform.position, transform.rotation);
      rBDust = dustObject.GetComponent<Rigidbody>();
      // float dustVelX = Mathf.Sin(i);
      float dustVelX =  Random.Range(-3, 3);
      // float dustVelY = Mathf.Sin(i);
      float dustVelY =  Random.Range(-3, 3);
      rBDust.linearVelocity = new Vector3(dustVelX, dustVelY, 0);
      Destroy(dustObject, dustDuration);
    }
  }
}
