using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewShift : MonoBehaviour
{
    public GameObject viewerCam;
    public Transform targetTransform;

    enum TriggerState
    {
        inState,
        outState
    }

    public enum TriggerType
    {
        stageTrigger,
        upTrigger,
        downTrigger,
        horzTrigger,
        vertTrigger
    }

    private void Start ()
    {
        
    }

    TriggerState triggerState = TriggerState.inState;
    public TriggerType inSetting;
    public TriggerType outSetting; 

    private void OnTriggerEnter(Collider other)
    {
       
        print("Entry Collision");
        
        if (other.CompareTag("GuyBase"))
        {
            if (triggerState == TriggerState.inState)
            {

                if (inSetting == TriggerType.downTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftDown();
                    print("Entry Trigger, ShiftDown");
                }

                else if (inSetting == TriggerType.stageTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftStage();
                    print("Entry Trigger, ShiftUp");
                }

                else if (inSetting == TriggerType.horzTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftHorz();
                    viewerCam.GetComponent<viewer>().entryTriggerTarget = this.targetTransform.position;
                    print("Entry Trigger, ShiftLeft");
                }

            }

            if (triggerState == TriggerState.outState)
            {

                if (outSetting == TriggerType.downTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftDown();
                    print("Entry Trigger, ShiftDown");
                }

                else if (outSetting == TriggerType.stageTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftStage();
                    print("Entry Trigger, ShiftStage");
                }

                else if (outSetting == TriggerType.horzTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftHorz();
                    print("Entry Trigger, ShiftHorz");
                }

                else if (outSetting == TriggerType.vertTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftVert();
                    print("Entry Trigger, ShiftVert");
                }

            }

            changeState();

        }


        
    }

    private void changeState()
    {
        if (triggerState == TriggerState.inState)
        {
            triggerState = TriggerState.outState;
            print("exit state");
        } else
        {
            triggerState = TriggerState.inState;
            print("init state");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        targetTransform = transform.GetChild(0);
    }
}
