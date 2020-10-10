using UnityEngine;

public class OrbCollisions : MonoBehaviour
{
    Rigidbody rB;
    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SpikePellet")
        {
            rB.isKinematic = false;
            rB.useGravity = true;
            rB.mass = 2f;
            Destroy(this.gameObject, 1f);
            hit = true;
        }
    }

}
