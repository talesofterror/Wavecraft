﻿using UnityEngine;

public class WallOsc : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(5f, 0, 0); //direction of movement
    [SerializeField] float period = Random.Range(1f,3f);
    Vector3 startingPos;

    [SerializeField] [Range(0, 1)] float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f; //equal about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        Mathf.Clamp(rawSinWave, 0.9f, 5000);

        movementFactor = rawSinWave / 2f + 0.5f;


        Vector3 offset = movementFactor * movementVector; // uses the movement factor (which is the movement increment) to applify the movement vector (which is set to the "right" direction)
        transform.position = startingPos + offset; // states that the objects position should equal the starting position plus the product of the vector direction and movement increment
    }
}