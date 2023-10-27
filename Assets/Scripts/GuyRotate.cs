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

  Vector3 leftRotation;
  Vector3 rightRotation;
  Vector3 currentRotation;

  public Direction direction = Direction.FacingRight;


  void Start()
  {
    // rightRotation = new Vector3(transform.rotation.x, -75, transform.rotation.z);
    // leftRotation = new Vector3(transform.rotation.x, 75, transform.rotation.z);

    // rightRotation = new Vector3(0, -75, 0);
    // leftRotation = new Vector3(0, 75, 0);

 
  }

  void Update()
  {
    rightRotation = new Vector3(transform.rotation.x, -75, transform.rotation.z);
    leftRotation = new Vector3(transform.rotation.x, 75, transform.rotation.z);
    Vector3 currentRotationParent = transform.parent.transform.eulerAngles;
    Vector3 rightRotationParent = new Vector3(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.z);
    Vector3 leftRotationParent = new Vector3(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.z);

    if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("DPad-Horizontal") < 0)
    {

      if (direction == Direction.FacingLeft | direction == Direction.TurningLeft)
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

    if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("DPad-Horizontal") > 0)
    {
      if (direction == Direction.FacingRight | direction == Direction.TurningRight)
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

    if (Input.GetKeyDown(KeyCode.T) || Input.GetAxis("DPad-Horizontal") > 0)
    {
      print(this.transform.parent.transform.eulerAngles.z);
    }

  }

  IEnumerator applyrotationLeft(Vector3 currentRotationParent, Vector3 leftRotationParent)
  {
    print(leftRotation);
    float i;
    currentRotation = transform.eulerAngles;
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
    print(rightRotation);
    float i;
    Vector3 currentRotation = transform.eulerAngles;
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






