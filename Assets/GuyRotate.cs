using System;
using UnityEngine;


public class GuyRotate : MonoBehaviour
{
    // [SerializeField] public float lateralThrust = 100f;
    // [SerializeField] public float rscThrust = 30f;
    // public float rcsThrust = 100f;
    // Start is called before the first frame update

    public float speed = 180f;
    bool twistLeft = false;

    GameObject guyParent;
    GameObject guyChild;
    Transform rocketTransform;
    Rigidbody rigidBody;
    Transform twistClamp;

    Vector3 twistVel;


    void Start()
    {        
        guyParent = GameObject.FindGameObjectWithTag("GuyBase");
        rocketTransform = guyParent.GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
        twistClamp = GetComponent<Transform>();
        twistVel = new Vector3(0f, 137, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //print(Time.deltaTime * speed);
        // OldMethod();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Mathf.Clamp(transform.rotation.y, 180, 0);
            //rigidBody.AddRelativeForce(new Vector3(0f, speed, 0f));
            //transform.rotation = Quaternion.Euler(new Vector3(0f, speed * Time.deltaTime, 0f));
            //print(transform.rotation.y);

            Quaternion deltaRot = Quaternion.Euler(twistVel * Time.deltaTime);
            rigidBody.MoveRotation(rigidBody.rotation * deltaRot);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //twistLeft = false;
            //float twist = speed * Time.deltaTime;

            //rigidBody.AddRelativeForce(new Vector3(0f, speed, 0f));
            //Mathf.Clamp(twist, 50, 0);
            
        }

    }

    /*
    private void OldMethod()
    {
        float yRotate = lateralThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(Vector3.up * yRotate / -7);
            //transform.Rotate(Vector3.up / Mathf.Sin(-yRotate/2));
            //transform.Rotate(Vector3.up * (transform.position.x * 2));
            transform.Rotate(Vector3.forward * rscThrust);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(Vector3.down * yRotate / -7);
            //transform.Rotate(Vector3.down / Mathf.Sin(-yRotate/2));
            //transform.Rotate(Vector3.down * (transform.position.x * 2));
            transform.Rotate(Vector3.back * rscThrust);
        }
    }

    */

}
