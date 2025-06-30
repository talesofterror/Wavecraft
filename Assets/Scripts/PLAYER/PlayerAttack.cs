using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [HideInInspector] public bool attackEnabled = true;
  private Vector3 projectileHeading;
  private float projectileDistance;
  private Vector3 projectileDirection;
  private Vector3 projectileMovement;
  private Transform pointerTransform;
  public int ammoPoolSize = 10;
  private GameObject[] ammoPool;
  private MeshRenderer[] ammoRenderers;
  private Collider[] ammoColliders;
  private Rigidbody[] ammoRB;
  private AmmoCollisions[] ammoCollisions;
  private int ammoIndex = 0;
  public GameObject projectile;
  public float projectileSpeed = 10;


  // Start is called before the first frame update
  void Awake()
  {
    ammoPool = new GameObject[ammoPoolSize];
    ammoRenderers = new MeshRenderer[ammoPoolSize];
    ammoColliders = new Collider[ammoPoolSize];
    ammoRB = new Rigidbody[ammoPoolSize];
    ammoCollisions = new AmmoCollisions[ammoPoolSize];
    pointerTransform = GameObject.FindGameObjectWithTag("Pointer").transform;

    for (int i = 0; i < ammoPoolSize; i++)
    {
      ammoPool[i] = GameObject.Instantiate(projectile, transform.position + new Vector3(0, 0, 0), projectile.transform.rotation);
      ammoRenderers[i] = ammoPool[i].GetComponent<MeshRenderer>();
      ammoRB[i] = ammoPool[i].GetComponent<Rigidbody>();
      ammoColliders[i] = ammoPool[i].GetComponent<Collider>();
      ammoCollisions[i] = ammoPool[i].GetComponent<AmmoCollisions>();
      ammoRenderers[i].enabled = false;
      ammoColliders[i].enabled = false;
      ammoPool[i].name = "SpikePellet " + i;
    }

  }

  void FixedUpdate()
  {
    projectileHeading = pointerTransform.position - transform.position;
    projectileDistance = projectileHeading.magnitude;
    projectileDirection = projectileHeading / projectileDistance;
    projectileMovement = projectileDirection * projectileSpeed;
  }
  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0) && PLAYERSingleton.i.attackEnabled)
    {
      fireProjectile();
    }
  }

  public void fireProjectile()
  {
    if (ammoIndex == ammoPoolSize)
    {
      ammoIndex = 0;
    }
    ammoPool[ammoIndex].transform.position = pointerTransform.position;
    ammoRenderers[ammoIndex].enabled = true;
    ammoColliders[ammoIndex].enabled = true;
    ammoRB[ammoIndex].linearVelocity = projectileMovement;
    ammoCollisions[ammoIndex].playerPositionWhenFired = PLAYERSingleton.i.transform.position;
    ammoIndex++;
  }

}
