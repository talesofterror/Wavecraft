﻿using System;
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
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            print("Thrusting");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            print("Rotate Right");
        }
    }
}
