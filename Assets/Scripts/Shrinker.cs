using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
    public float speed = 1.5f;
    public float scaleRange = 0.5f;

    Vector3 scaleChange;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time * speed;

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau) * scaleRange;
        float scaleFactor = rawSinWave / 2f + 0.5f;

        scaleChange = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        transform.localScale = scaleChange;
    }
}
