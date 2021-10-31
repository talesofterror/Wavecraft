using System.Collections;
using UnityEngine;


public class GuyRotate : MonoBehaviour
{
    public enum Direction
    {
        TurningLeft,
        TurningRight,
        FacingLeft,
        FacingRight
    };

    public float rcsThrust;

    public GameObject GuyObject;
    Rigidbody rB;

    Vector3 leftRotation;
    Vector3 rightRotation;

    Direction direction = Direction.TurningRight;


    void Start()
    {
        rB = GuyObject.GetComponent<Rigidbody>();

        


    }

    // Update is called once per frame
    void Update()
    {
        rightRotation = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        leftRotation = new Vector3(transform.rotation.x, -180, transform.rotation.z);

        print(rightRotation);

        //print(Time.deltaTime * speed);
        //OldMethod();

        if (Input.GetKeyDown(KeyCode.A))
        {

            direction = Direction.TurningLeft;

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Direction.TurningRight;

        }



        if (direction == Direction.TurningLeft)
        {
            StartCoroutine(TurnLeft());
        }
        if (direction == Direction.TurningRight)
        {
            StartCoroutine(TurnRight());
        }


    }

    IEnumerator TurnRight()
    {
        //rB.freezeRotation = true;
        transform.eulerAngles = rightRotation;
        //rightRotation.x = transform.eulerAngles.x;
        direction = Direction.FacingRight;
        //rB.freezeRotation = false;

        yield return null;
    }

    IEnumerator TurnLeft()
    {
        //rB.freezeRotation = true;
        transform.eulerAngles = leftRotation;
        //leftRotation.x = transform.eulerAngles.x;
        direction = Direction.FacingLeft;
        //rB.freezeRotation = false;

        yield return null;
    }





private void OldMethod()
{

    if (Input.GetKey(KeyCode.RightArrow))
    {

        transform.Rotate(Vector3.forward * rcsThrust);

    }
    else if (Input.GetKey(KeyCode.LeftArrow))
    {

        transform.Rotate(Vector3.back * rcsThrust);
    }
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