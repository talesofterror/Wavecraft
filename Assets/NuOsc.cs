using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuOsc : MonoBehaviour
{
    Vector3 worldPos;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        worldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.localPosition.x;
        float y = Mathf.Sin(Time.time * 2) * 1;
        float z = transform.localPosition.z;

        offset = new Vector3(x, y, z);
        transform.position = worldPos + offset;
    }
}
