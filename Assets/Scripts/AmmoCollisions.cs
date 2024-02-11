using UnityEngine;

public class AmmoCollisions : MonoBehaviour
{
  public GameObject dustBall;
  public EnemyStats targetStats;
  public float damage = 10;
  public int dustAmount = 3;
  Rigidbody rB;
  Rigidbody rBDust;
  Vector3 ammoLoc;
  MeshRenderer mrenderer;

  // Start is called before the first frame update
  void Awake()
  {
    mrenderer = GetComponent<MeshRenderer>();
    rB = this.gameObject.GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter(Collision collision)
  {
    mrenderer.enabled = false;

    targetStats = collision.gameObject.GetComponent<EnemyStats>();

    if (collision.gameObject.tag == "Enemy")
    {
      print(transform.name + ": " + targetStats.enemyHP);
      targetStats.enemyHP -= damage;
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
      GameObject dustObject = Instantiate(dustBall, ammoLoc, rB.rotation);
      rBDust = dustObject.GetComponent<Rigidbody>();
      float dustVelX = Mathf.Sin(i + 1);
      float dustVelY = Mathf.Sin(i + 1);
      rBDust.velocity = new Vector3(dustVelX, dustVelY, 0);
      Destroy(dustObject, 0.2f);
    }
  }
}
