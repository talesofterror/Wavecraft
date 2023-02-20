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
    public float yPos = 0f;
    public float xPosOffset = 0f;
    public float yPosOffset = 0f;
    public float xRot = 0f;
    public float yRot = 0f;
    public float zRot = 0f;
    public float panSpeed = 2f;

    public float vertMidOffset = 6f;
    public float horzMidOffset = 6f;

    enum CamState
    {
        stageView, 
        downAnim,
        vertMiddle, 
        upAnim,
        leftAnim,
        horzMiddle,
        rightAnim
    }

    CamState state = CamState.vertMiddle;

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
        state = CamState.downAnim;
    }

    public void ShiftUp()
    {
        state = CamState.upAnim;
    }

    public void ShiftLeft()
    {
        state = CamState.leftAnim;
    }

    public void ShiftRight()
    {
        state = CamState.rightAnim;
    }

    public void downAnimPlay()
    {
        // transform.position = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + viewShiftAnim, distance);

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + vertMidOffset, distance);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
        if (transform.position == endPos)
        {
            state = CamState.vertMiddle;
        }
    }

    public void stageAnimPlay ()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(playerPos.transform.position.y -yPosOffset + horzMidOffset, yPos, distance);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
        if (transform.position == endPos)
        {
            state = CamState.stageView;
        }
    }

    public void leftAnimPlay ()
    {
        yPos = -40.29f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(playerPos.transform.position.x - xPosOffset + horzMidOffset, yPos, distance);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
        if (transform.position == endPos)
        {
            state = CamState.horzMiddle;
        }
    }

    public void rightAnimPlay()
    {
        print("rightAnimPlay called");

        yPos = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + vertMidOffset, distance);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
        if (transform.position == endPos)
        {
            state = CamState.vertMiddle;
        }
    }

    void Update()
    {
    
        if (state == CamState.stageView)
        {
            transform.position = new Vector3(xPos, playerPos.transform.position.y + yPosOffset, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);

            Camera.main.fieldOfView = m_fieldOfView;
        }
        if (state == CamState.downAnim)
        {
                downAnimPlay();
        }

        if (state == CamState.vertMiddle) 
        {
            transform.position = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + vertMidOffset, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        }

        if(state == CamState.upAnim)
        {
            stageAnimPlay();
        }

        if(state == CamState.leftAnim)
        {
            leftAnimPlay();
        }

        if(state == CamState.rightAnim)
        {
            rightAnimPlay();
        }

        if (state == CamState.horzMiddle)
        {
            transform.position = new Vector3(playerPos.transform.position.x - xPosOffset + vertMidOffset, yPos, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        }

    }
}
