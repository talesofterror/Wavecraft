using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LookAt : MonoBehaviour
{
  public GameObject target;
  Vector3 targetTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        targetTransform = target.transform.position;
        Vector3 lookTarget = new Vector3(1f, 1f, 1f);
        transform.LookAt(targetTransform + lookTarget, Vector3.down);
        transform.rotation = Quaternion.Euler(0, 0, -180);
    }
}
