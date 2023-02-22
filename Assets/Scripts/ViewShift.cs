using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewShift : MonoBehaviour
{
    public GameObject viewerCam;

    enum View
    {
        vertView, 
        stageView,
        horzView
    };

    enum TriggerState
    {
        initState,
        exitState
    }

    public enum TriggerType
    {
        stageTrigger,
        upTrigger,
        downTrigger,
        leftTrigger,
        rightTrigger
    }

    // at this point activeView must be set to stageView if starting at top stage
    // and vertView if starting somewhere within the cave
    View activeView = View.vertView;

    TriggerState triggerState = TriggerState.initState;
    public TriggerType inSetting;
    public TriggerType outSetting; 

    private void OnTriggerEnter(Collider other)
    {

        /*
         * The trigger State isn't changing and I'm not sure why.
         * 
         * Maybe it would work if I call the state change from the viewer script, at the end of the animations. 
         */
        

        print("Entry Collision");
        
        if (other.CompareTag("GuyBase"))
        {
            if (triggerState == TriggerState.initState)
            {

                if (inSetting == TriggerType.downTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftDown();
                    activeView = View.vertView;                         // "View"/"activeView" state may not be needed
                    print("Entry Trigger, ShiftDown");
                }

                else if (inSetting == TriggerType.stageTrigger) // need a way to trigger this if it's in any other state
                {
                    viewerCam.GetComponent<viewer>().ShiftUp();
                    activeView = View.stageView;
                    print("Entry Trigger, ShiftUp");
                }

                else if (inSetting == TriggerType.leftTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftLeft();
                    activeView = View.horzView;
                    print("Entry Trigger, ShiftLeft");
                }

            }

            if (triggerState == TriggerState.exitState)
            {

                if (outSetting == TriggerType.downTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftDown();
                    activeView = View.vertView;                         // "View" state may not be needed
                    print("Entry Trigger, ShiftDown");
                }

                else if (outSetting == TriggerType.stageTrigger) // need a way to trigger this if it's in any other state
                {
                    viewerCam.GetComponent<viewer>().ShiftUp();
                    activeView = View.stageView;
                    print("Entry Trigger, ShiftUp");
                }

                else if (outSetting == TriggerType.leftTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftLeft();
                    activeView = View.horzView;
                    print("Entry Trigger, ShiftLeft");
                }

                else if (outSetting == TriggerType.rightTrigger)
                {
                    viewerCam.GetComponent<viewer>().ShiftRight();
                    activeView = View.vertView;
                    print("Entry Trigger, ShiftRight");
                }

                // triggerState = TriggerState.initState;

            }

            changeState();

        }


        
    }

    private void changeState()
    {
        if (triggerState == TriggerState.initState)
        {
            triggerState = TriggerState.exitState;
            print("exit state");
        } else
        {
            triggerState = TriggerState.initState;
            print("init state");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
