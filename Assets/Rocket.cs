using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;

    // was wondering what this value looked like. It just repeatedly prints zero. Add
    int number = 2;
    float deltaTimeValue = Time.deltaTime;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        PlusMinus();
        DeltaPrint();
    }

    void DeltaPrint()
    {
        print(deltaTimeValue);
        Console.Clear(); 
    }

    // Update is called once per frame
    void Update()
    {
        RocketControls();
        DeltaPrint();
    }

    void PlusMinus()
    {
        number = number + 2; // by which I mean: x = x + y
        print(number);
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
