using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float thrustPower = 100f;

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
        SoundControl();
        Thrust();
        Rotate();
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

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = thrustPower * Time.deltaTime;
            rigidBody.freezeRotation = true;
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            rigidBody.freezeRotation = false;
            // rocketSound.UnPause();    ---- Did not work. 
        }
    }

    private void Rotate()
    {

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        else
        {
            // rocketSound.Pause();
        }

        rigidBody.freezeRotation = false;

    }

}


// Side to side movement. transform for the sake of posterity. 
//
// if (Input.GetKey(KeyCode.LeftArrow))
// {
//     transform.position += (Vector3.left * Time.deltaTime);
// }