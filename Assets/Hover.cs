using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector = new Vector3(0, .9f, 0); //direction of movement
    public float period = 2f;
    [Range (0,1)] public float movementFactor;

    Rigidbody rB;
    

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / period;
        float tau = Mathf.PI * 2f;
        float rawOsc = Mathf.Sin(cycle * tau);
        movementFactor = rawOsc / 1f + 0f;
        Vector3 offset = movementFactor * movementVector;
        startingPos = transform.localPosition;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = startingPos + offset;
            rB.isKinematic = true;
        }
        else
        {
            rB.isKinematic = false;
        }
    }
}
