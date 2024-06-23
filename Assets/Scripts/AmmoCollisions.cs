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

  // Start is called before the first frame update
  void Awake()
  {
    mrenderer = GetComponent<MeshRenderer>();
    mcollider = GetComponent<Collider>();
    rB = this.gameObject.GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter(Collision collision)
  {

    mrenderer.enabled = false;

    if (collision.gameObject.tag == "Enemy")
    {

      mcollider.enabled = false;
      enemyStats = collision.gameObject.GetComponent<EnemyStats>();
      enemyStats.hP -= damage;
      print(collision.gameObject.name + ": " + enemyStats.hP + "HP -- took " + damage + " damage");
    }

    for (int i = 1; i <= dustAmount; ++i)
    {
      dustBloom(i);
    }
  }

  private void dustBloom(int i)
  {
    if (i <= dustAmount)
    {
      // float dustLoc = Mathf.Sin(0.5f + i);
      // ammoLoc = transform.position + new Vector3(0.5f, dustLoc, 0f);
      GameObject dustObject = Instantiate(dustBall, transform.position, rB.rotation);
      rBDust = dustObject.GetComponent<Rigidbody>();
      float dustVelX = Mathf.Sin(i + 1);
      float dustVelY = Mathf.Sin(i + 1);
      rBDust.velocity = new Vector3(dustVelX, dustVelY, 0);
      Destroy(dustObject, dustDuration);
    }
  }
}
