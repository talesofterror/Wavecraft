using UnityEngine;

public class DustScript : MonoBehaviour
{
    Rigidbody rB;
    private void Awake()
    {
        rB = GetComponent<Rigidbody>();
        //rB.AddExplosionForce(1, transform.position, 5f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
