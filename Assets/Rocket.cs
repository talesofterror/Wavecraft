using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        RocketControls();
    }

    private void RocketControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
        }

        // Side to side movement. transform 
        //
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     transform.position += (Vector3.left * Time.deltaTime);
        // }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back);
        }
    }
}
