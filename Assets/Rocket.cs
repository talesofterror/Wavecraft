using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource rocketSound;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketControls();
        SoundControl();
    }

    private void SoundControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketSound.UnPause();
        }
        else
        {
            rocketSound.Pause();
        }

    }

        private void RocketControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            // rocketSound.UnPause();    ---- Did not work. 
        }

        // Side to side movement. transform for the sake of posterity. 
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
        else
        {
            // rocketSound.Pause();
        }
    }
}
