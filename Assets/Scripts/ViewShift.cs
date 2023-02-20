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

    public enum TriggerType
    {
        stagetrigger,
        upTrigger,
        downTrigger,
        leftTrigger,
        rightTrigger
    }


    // at this point activeView must be set to stageView if starting at top stage
    // and vertView if starting somewhere within the cave
    View activeView = View.vertView;

    public TriggerType triggerType;

    private void OnTriggerEnter(Collider other)
    {

        print("Entry Collision");
        
        if (other.CompareTag("GuyBase"))
        {
            if (triggerType == TriggerType.downTrigger)
            {
                viewerCam.GetComponent<viewer>().ShiftDown();
                activeView = View.vertView;
                print("Entry Trigger, ShiftDown");
            }


            else if (triggerType == TriggerType.stagetrigger) // need a way to trigger this if it's in any other state
            {
                viewerCam.GetComponent<viewer>().ShiftUp();
                activeView = View.stageView;
                print("Entry Trigger, ShiftUp");
            }

            else if (triggerType == TriggerType.leftTrigger)
            {
                viewerCam.GetComponent<viewer>().ShiftLeft();
                activeView = View.horzView;
                print("Entry Trigger, ShiftLeft");
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
