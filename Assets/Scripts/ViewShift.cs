using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewShift : MonoBehaviour
{
    public GameObject viewerCam;

    enum View
    {
        camDown, 
        camUp
    };

    View activeView = View.camUp;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("GuyBase"))
        {
            if (activeView == View.camUp)
            {
                viewerCam.GetComponent<viewer>().ShiftDown();
                activeView = View.camDown;
                print("Entry Trigger, ShiftDown");
            }


            else if (activeView == View.camDown)
            {
                viewerCam.GetComponent<viewer>().ShiftUp();
                activeView = View.camUp;
                print("Entry Trigger, ShiftUp");
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
