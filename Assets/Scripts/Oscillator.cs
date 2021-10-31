using System.Security.Cryptography;
using UnityEngine;


public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(5f, 0, 0); //direction of initial movement
    [SerializeField] float period = 2f; // speed of oscillation
    Vector3 startingPos; // starting transform snatched at Start()

    [Range (0,1)] float movementFactor; // gets added to the movementVector

    public bool on = false;

    float seed; // randomness defined in Start()

    void Start()
    {
        startingPos = transform.position;
        seed = Random.Range(1f, 1.9f);
    }


    /*
    public float Randomness(float tau)
    {
        float result;

        result = tau * Random.Range(1f, 1.9f);
        

        return result;
    } 
    */

    // Update is called once per frame
    void Update()
    {
        if (on == true)
        {
            float cycles = Time.time / period * seed;

            const float tau = Mathf.PI * 2f; //equal about 6.28
                                             // seed = Randomness(tau);
            float rawSinWave = Mathf.Sin(cycles * tau);
            Mathf.Clamp(period, 0.9f, 5000);

            movementFactor = rawSinWave / 2f + 0.5f;


            Vector3 movement = movementFactor * movementVector; // uses the movement factor (which is the movement increment) to applify the movement vector (which is set to the "right" direction)
            transform.position = startingPos + movement; // states that the objects position should equal the starting position plus the product of the vector direction and movement increment
        }
        else
        {
            return;
        }

    }
}
