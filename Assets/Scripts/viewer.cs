﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * TO DO: 
 * 
 * Refactor: Lots of issues
 * 
 * Add an object to act as the y position target for each trigger object
 * 
 * VARIABLES THAT DO NOTHING: 
 * - m_fieldOfView
 * - yPos
 * - xPosOffset
 * - horzMidOffset
 * 
 * How do I get the value of the target position from the ViewShift into this script? 
 * 
 * Instead of getting the target location from the Rocket script I should try getting it from
 * the trigger object itself. Send a message from ViewShift to Viewer (which is already happening)
 * to specify the y location
 * 
 */


public class viewer : MonoBehaviour
{
    Vector3 stageVector;
    Transform swivelTarget;
    GameObject playerObject;
    public Vector3 entryTriggerTarget;

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
        stageAnim,
        horzAnim,
        horzMiddle,
        vertAnim
    }

    CamState state = CamState.stageView;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        //entryTriggerTarget = playerObject.GetComponent<Collider>().gameObject;

        transform.position = new Vector3(xPos, playerObject.transform.position.y + yPosOffset, distance);
        stageVector = transform.position;
    }

    public void ShiftDown()
    {
        state = CamState.downAnim;
    }

    public void ShiftStage()
    {
        state = CamState.stageAnim;
    }

    public void ShiftHorz()
    {
        state = CamState.horzAnim;
    }

    public void ShiftVert()
    {
        state = CamState.vertAnim;
    }

    public void downAnimPlay()
    {
        // transform.position = new Vector3(xPos, playerPos.transform.position.y - yPosOffset + viewShiftAnim, distance);
        yPosOffset = 5;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(xPos, playerObject.transform.position.y - yPosOffset + vertMidOffset, distance);
        Swivel();

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
        if (transform.position == endPos)
        {
            state = CamState.vertMiddle;
        }
    }

    private void Swivel()
    {
        swivelTarget = playerObject.transform;
        transform.LookAt(swivelTarget);
    }

    public void stageAnimPlay ()
    {
        //yPosOffset = 4.51f;
        Vector3 startPos = transform.position;
        Vector3 endPos = stageVector;

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);

            state = CamState.stageView;

    }

    public void horzAnimPlay ()
    {

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(playerObject.transform.position.x - xPosOffset + horzMidOffset, entryTriggerTarget.y, distance);
        swivelTarget = playerObject.transform;
        transform.LookAt(swivelTarget);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
        if (transform.position == endPos)
        {
            state = CamState.horzMiddle;
        }
    }

    public void vertAnimPlay()
    {
        print("rightAnimPlay called");

        yPos = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(xPos, playerObject.transform.position.y - yPosOffset + vertMidOffset, distance);
        swivelTarget = playerObject.transform;
        transform.LookAt(swivelTarget);

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
            transform.position = new Vector3(xPos, playerObject.transform.position.y + yPosOffset, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);

            Camera.main.fieldOfView = m_fieldOfView;
        }
        if (state == CamState.downAnim)
        {
                downAnimPlay();
        }

        if (state == CamState.vertMiddle) 
        {
            transform.position = new Vector3(xPos, playerObject.transform.position.y - yPosOffset + vertMidOffset, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        }

        if(state == CamState.stageAnim)
        {
            stageAnimPlay();
        }

        if(state == CamState.horzAnim)
        {
            horzAnimPlay();
        }

        if(state == CamState.vertAnim)
        {
            vertAnimPlay();
        }

        if (state == CamState.horzMiddle)
        {
            transform.position = new Vector3(playerObject.transform.position.x - xPosOffset + vertMidOffset, yPos, distance);
            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        }

    }
}