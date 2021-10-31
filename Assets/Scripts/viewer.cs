using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class viewer : MonoBehaviour
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

    public float caveViewOffset = 6f;

    bool initialView = true;
    bool caveViewLow = false;
    bool downAnimtrue = false;

    void Start()
    {
        camBody = GetComponent<Rigidbody>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        rocketScript = playerPos.GetComponent<Rocket>();
        camObject = GetComponent<Transform>();

        transform.position = new Vector3(xPos, playerPos.transform.position.y + yPosOffset, distance);
    }

    public void ShiftDown()
    {
        initialView = false;
        caveViewLow = true;
        downAnimtrue = true;
    }

    public void ShiftUp()
    {
        initialView = true;
        caveViewLow = false;
    }

    public void downAnimPlay()
    {
        // transform.position = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + viewShiftAnim, distance);

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + caveViewOffset, distance);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * 2f);
        if (transform.position == endPos)
        {
            downAnimtrue = false;
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

        /*
         *       CAMERA POSITION IS ALWAYS OFFSET BY yPosOffset *****
        */

        

        if (initialView == true)
        {
            transform.position = new Vector3(xPos, playerPos.transform.position.y + yPosOffset, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);

            Camera.main.fieldOfView = m_fieldOfView;
        }
        if (caveViewLow == true)
        {
            if (downAnimtrue == true)
            {
                downAnimPlay();
            }

            if (downAnimtrue == false) 
            {
                transform.position = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + caveViewOffset, distance);
                transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
            }


            Camera.main.fieldOfView = m_fieldOfView;
        }

    }
}
