using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelletforce : MonoBehaviour
{
    Rigidbody rB;
    GameObject guyBase;

    // Start is called before the first frame update
    void Start()
    {
        guyBase = GameObject.FindGameObjectWithTag("GuyBase");
    }

    private void Awake()
    {
        rB = GetComponent<Rigidbody>();
        //rB.velocity = new Vector3(5f, 1.5f, 0f) * 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "GuyBase")
        {
            Physics.IgnoreCollision(guyBase.GetComponent<BoxCollider>(), gameObject.GetComponent<BoxCollider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
