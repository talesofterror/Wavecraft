using UnityEngine;

public class AmmoCollisions : MonoBehaviour
{
    public GameObject dustBall;
    Rigidbody rBDust;
    Rigidbody rB;
    Vector3 ammoLoc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" | collision.gameObject.tag == "SpikePellet" | collision.gameObject.tag == "GuyBase")
        {
            for (int i = 1; i <= 5; ++i)
            {
                if (i <= 5)
                {
                    rB = this.gameObject.GetComponent<Rigidbody>();
                    float dustLoc = Mathf.Sin(0.5f + i);
                    ammoLoc = transform.position + new Vector3(0.5f, dustLoc, 0f);
                    GameObject dustObject = Instantiate(dustBall, ammoLoc, rB.rotation);
                    rBDust = dustObject.GetComponent<Rigidbody>();
                    float dustVelX = Mathf.Sin(i + 1);
                    float dustVelY = Mathf.Sin(i + 1);
                    rBDust.velocity = new Vector3(dustVelX, dustVelY, 0);
                    Destroy(dustObject, 2f);
                }
                
            }
            
        }  
    }

}
