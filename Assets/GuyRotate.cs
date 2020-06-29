using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuyRotate : MonoBehaviour
{
    GameObject guyParent;
    GameObject guyChild;
    Transform rocketTransform;
    Rigidbody rigidBody;


    [SerializeField] public float lateralThrust = 100f;

    public float rcsThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {        
        guyParent = GameObject.FindGameObjectWithTag("GuyBase");
        rocketTransform = guyParent.GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float yRotate = lateralThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(Vector3.up * yRotate / -7);
            //transform.Rotate(Vector3.up / Mathf.Sin(-yRotate/2));
            transform.Rotate(Vector3.up * (transform.position.x * 2));

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(Vector3.down * yRotate / -7);
            //transform.Rotate(Vector3.down / Mathf.Sin(-yRotate/2));
            transform.Rotate(Vector3.down * (transform.position.x * 2));

        }
    }
}
