using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ViewerScript : MonoBehaviour
{
    Rigidbody camBody;
    GameObject playerPos;
    Rocket rocketScript;
    Transform camObject;
    OldCamera cam;

    public float m_fieldOfView = 0f;
    public float distance = -45f;
    public float xPos = 0f;
    public float yPosOffset = 0f;
    public float xRot = 0f;
    public float yRot = 0f;
    public float zRot = 0f;

    void Start()
    {
        camBody = GetComponent<Rigidbody>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        rocketScript = playerPos.GetComponent<Rocket>();
        camObject = GetComponent<Transform>();

        // RocketDebug();
        // DebugListComponents();
    }

    void RocketDebug()
    {
        if (rocketScript == null)
        {
            print("Nothing there, bro.");
        }
        else
        {
            print("Everything seems to be in order.");
        }
    }

    void Update()
    {
        // float thrustThisFrame = rocketScript.thrustPower * Time.deltaTime;

        // if (Input.GetKey(KeyCode.Space))
        // {
        //    camBody.isKinematic = false;
        //    camBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        // }
        // else
        // {
        //    camBody.isKinematic = true;
        // }

        // float rocketPostion = rocketShip.transform.position.y;
        // camBody.MovePosition(transform.position + rocketPostion);

        // ^ none of this shit worked smh

        transform.position = new Vector3(xPos, playerPos.transform.position.y + yPosOffset, distance);
        transform.rotation = Quaternion.Euler(xRot, yRot, zRot);

    }
}
