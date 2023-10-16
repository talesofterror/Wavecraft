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

  Direction direction = Direction.FacingRight;


  void Start()
  {
    rightRotation = new Vector3(transform.rotation.x, -75, transform.rotation.z);
    leftRotation = new Vector3(transform.rotation.x, 75, transform.rotation.z);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.A))
    {
      if (direction == Direction.FacingLeft | direction == Direction.TurningLeft)
      {
        return;
      }
      else
      {
        StopAllCoroutines();
        StartCoroutine(applyrotationLeft());
        direction = Direction.TurningLeft;
      }
    }

    if (Input.GetKeyDown(KeyCode.D))
    {
      if (direction == Direction.FacingRight | direction == Direction.TurningRight)
      {
        return;
      }
      else
      {
        StopAllCoroutines();
        StartCoroutine(applyrotationRight());
        direction = Direction.TurningRight;
      }
    }


    //     if (direction == Direction.TurningLeft)
    //     {
    //       if (transform.eulerAngles != leftRotation)
    //       {
    //         return;
    //       }
    //       else
    //       {
    //         direction = Direction.FacingLeft;
    //       }
    //     }
    //     if (direction == Direction.TurningRight)
    // {
    //   if (transform.eulerAngles != rightRotation)
    //   {
    //     return;
    //   }
    //   else
    //   {
    //     direction = Direction.FacingRight;
    //   }
    // }

    // if (direction == Direction.FacingLeft)
    // {
    //   transform.eulerAngles = leftRotation;
    // }
    // if (direction == Direction.FacingRight)
    // {
    //   transform.eulerAngles = rightRotation;
    // }

  }

  IEnumerator applyrotationLeft()
  {
    float i;
    Vector3 currentRotation = transform.eulerAngles;
    for (i = 0; i <= 1; i += 1 * Time.deltaTime)
    {
      transform.eulerAngles = Vector3.Lerp(currentRotation, leftRotation, i);
      print("rotation left: " + i);
      yield return null;
    }
  }

  IEnumerator applyrotationRight()
  {
    float i;
    Vector3 currentRotation = transform.eulerAngles;
    for (i = 0; i <= 1; i += 1 * Time.deltaTime)
    {
      transform.eulerAngles = Vector3.Lerp(currentRotation, rightRotation, i);
      yield return null;
    }
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






