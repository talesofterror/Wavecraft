using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    [SerializeField] Vector3 scaleVector = new Vector3(1.2f, 1.2f, 1.2f); //direction of initial movement
    [SerializeField] float period = 2f; // speed of oscillation
    Vector3 startingScale; // starting transform snatched at Start()

    [Range(0, 1)] float scaleFactor; // gets added to the movementVector

    float seed; // randomness defined in Start()


    // Start is called before the first frame update
    void Start()
    {
        startingScale = transform.localScale;
        //seed = Random.Range(1f, 1.9f);
        seed = 1;
    }

    // Update is called once per frame
    void Update()
    {

        float cycles = Time.time / period * seed;
        const float tau = Mathf.PI * 2f; //equal about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        Mathf.Clamp(period, 0.9f, 5000);

        scaleFactor = rawSinWave / 2f + 1f;

        Vector3 scaling = scaleFactor * scaleVector;
        transform.localScale = startingScale + scaling;

    }
}
