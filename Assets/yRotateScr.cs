using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yRotateScr : MonoBehaviour
{
    
    [SerializeField] public float rcsThrust = 100f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        transform.Rotate(0, 0, -rotationThisFrame);
    }
}
