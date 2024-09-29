using System;
using System.Collections;
using UnityEngine;


public class GuyRotate : MonoBehaviour
{
  [HideInInspector]
  public enum Direction
  {
    TurningLeft,
    TurningRight,
    FacingLeft,
    FacingRight
  };

  Vector3 leftRotation;
  Vector3 rightRotation;
  Vector3 currentRotation;

  public Direction direction = Direction.FacingRight;

  public float turnDelayThreshhold = 0.4f;

  private Rigidbody parentRigidBody;

  void Start()
  {
    parentRigidBody = transform.parent.gameObject.GetComponent<Rigidbody>();
  }
  void Update()
  {

    // float turnThrow;
    // void calculateThrow()
    // {

    // }

    float rBXVelocity = parentRigidBody.velocity.x;
    // print("rigidbody x velocity = " + rBXVelocity);
    // print("x velocity less than threshhold (-)?" + (rBXVelocity < -turnDelayThreshhold));
    // print("x velocity greater than threshhold (+)?" + (rBXVelocity > turnDelayThreshhold));

    rightRotation = new Vector3(transform.rotation.x, -75, transform.rotation.z);
    leftRotation = new Vector3(transform.rotation.x, 35, transform.rotation.z);

    Vector3 currentRotationParent = transform.parent.transform.eulerAngles;
    Vector3 rightRotationParent = new Vector3(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.z);
    Vector3 leftRotationParent = new Vector3(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.z);

    // * left 

    if (rBXVelocity < -turnDelayThreshhold)
    {
      if ((Input.GetKey(KeyCode.A)
          || Input.GetAxis("Horizontal") < -0.25f
          || Input.GetAxis("DPad-Horizontal") < -0.50f)
          && direction != Direction.TurningLeft
          )
      {
        if (direction == Direction.FacingLeft)
        {
          return;
        }
        else
        {
          StopAllCoroutines();
          StartCoroutine(applyrotationLeft(currentRotationParent, leftRotationParent));
          direction = Direction.TurningLeft;
        }
      }
    }

    // * right 

    if (rBXVelocity > turnDelayThreshhold)
    {
      if ((Input.GetKey(KeyCode.D)
          || Input.GetAxis("Horizontal") > 0.25f
          || Input.GetAxis("DPad-Horizontal") > 0.50f)
          && direction != Direction.TurningRight
          )
      {
        if (direction == Direction.FacingRight)
        {
          return;
        }
        else
        {
          StopAllCoroutines();
          StartCoroutine(applyrotationRight(currentRotationParent, rightRotationParent));
          direction = Direction.TurningRight;
        }
      }
    }

  }

  IEnumerator applyrotationLeft(Vector3 currentRotationParent, Vector3 leftRotationParent)
  {
    float i;
    currentRotation = transform.eulerAngles;
    yield return new WaitForSeconds(0.4f);
    for (i = 0; i <= 1; i += 1 * Time.deltaTime)
    {
      if (i >= 1)
      {
        direction = Direction.FacingLeft;
      }
      transform.eulerAngles = Vector3.Lerp(currentRotation, leftRotation, i);
      transform.parent.transform.eulerAngles = Vector3.Lerp(currentRotationParent, leftRotationParent, i);
      yield return null;
    }
  }

  IEnumerator applyrotationRight(Vector3 currentRotationParent, Vector3 rightRotationParent)
  {
    float i;
    Vector3 currentRotation = transform.eulerAngles;
    yield return new WaitForSeconds(0.4f);
    for (i = 0; i <= 1; i += 1 * Time.deltaTime)
    {
      if (i >= 1)
      {
        direction = Direction.FacingRight;
      }
      transform.eulerAngles = Vector3.Lerp(currentRotation, rightRotation, i);
      transform.parent.transform.eulerAngles = Vector3.Lerp(currentRotationParent, rightRotationParent, i);
      yield return null;
    }
  }

}






