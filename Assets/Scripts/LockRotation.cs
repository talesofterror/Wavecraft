using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Vector3 zeroVector;
    Quaternion zeroQuaternion;
    // Start is called before the first frame update
    void Start()
    {
        zeroVector = new Vector3(-90, 0, 0);
        zeroQuaternion.eulerAngles = zeroVector;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = zeroQuaternion;
    }
}
