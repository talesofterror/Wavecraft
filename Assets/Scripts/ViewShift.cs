using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewShift : MonoBehaviour
{
    public GameObject viewerCam;
    public Transform targetTransform;
    public float horzizontalYShift;
    public float verticalXShift;

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

    void Update()
    {
        targetTransform = transform.GetChild(0);
    }

    TriggerState triggerState = TriggerState.inState;
    public TriggerType inSetting;
    public TriggerType outSetting; 

    private void OnTriggerEnter(Collider other)
    {
       
      print("Entry Collision");

      Vector3 horizontalTarget = targetTransform.position + new Vector3(0, horzizontalYShift, 0);
      Vector3 verticalTarget = targetTransform.position + new Vector3(verticalXShift, 0, 0);

        
        if (other.CompareTag("GuyBase"))
        {
            if (triggerState == TriggerState.inState)
            {

                if (inSetting == TriggerType.downTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftDown();
                    print("Entry Trigger, Camera Shift Down");
                }

                else if (inSetting == TriggerType.stageTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftStage();
                    print("Entry Trigger, Stage");
                }

                else if (inSetting == TriggerType.horzTrigger)
                {
                    viewerCam.GetComponent<viewer>().getShiftTargetVector(horizontalTarget);
                    viewerCam.GetComponent<viewer>().ShiftHorz();
                    viewerCam.GetComponent<viewer>().entryTriggerTarget = this.targetTransform.position;
                    print("Entry Trigger, Horizontal");
                }

                else if (inSetting == TriggerType.vertTrigger)
                {
                    viewerCam.GetComponent<viewer>().getShiftTargetVector(verticalTarget);
                    viewerCam.GetComponent<viewer>().ShiftVert();
                    viewerCam.GetComponent<viewer>().entryTriggerTarget = this.targetTransform.position;
                    print("Entry Trigger, Vertical");
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
                    viewerCam.GetComponent<viewer>().getShiftTargetVector(horizontalTarget);
                    viewerCam.GetComponent<viewer>().ShiftHorz();
                    viewerCam.GetComponent<viewer>().entryTriggerTarget = this.targetTransform.position;
                    print("Entry Trigger, Horizontal");
                }

                else if (outSetting == TriggerType.vertTrigger)
                {
                    viewerCam.GetComponent<viewer>().getShiftTargetVector(verticalTarget);
                    viewerCam.GetComponent<viewer>().ShiftVert();
                    viewerCam.GetComponent<viewer>().entryTriggerTarget = this.targetTransform.position;
                    print("Entry Trigger, Vertical");
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
}
