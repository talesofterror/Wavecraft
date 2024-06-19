using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ViewShift : MonoBehaviour
{
  private viewer vCam;
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
    downTrigger,
    horzTrigger,
    vertTrigger
  }

  TriggerState triggerState = TriggerState.inState;
  public TriggerType inSetting;
  public TriggerType outSetting;

  public enum FOV
  {
    center,
    follow
  }

  public FOV fov = FOV.center;

  private void Start()
  {
    vCam = Camera.main.GetComponent<viewer>();
  }

  void Update()
  {
    targetTransform = transform.GetChild(0);

    if (fov == FOV.center) {
      vCam.swivelOn = false;
    } else {
      vCam.swivelOn = true;
    }
  }

  private void OnTriggerEnter(Collider other)
  {

    print("Entry Collision");

    Vector3 horizontalTarget = targetTransform.position + new Vector3(0, horzizontalYShift, 0);
    Vector3 verticalTarget = targetTransform.position + new Vector3(verticalXShift, 0, 0);


    if (other.CompareTag("GuyBase"))
    {
      if (triggerState == TriggerState.inState)
      {
        if (inSetting == TriggerType.stageTrigger)
        {
          vCam.ShiftStage();
          print("Entry Trigger, Stage");
        }
        else if (inSetting == TriggerType.horzTrigger)
        {
          vCam.getShiftTargetVector(horizontalTarget);
          vCam.ShiftHorz();
          vCam.entryTriggerTarget = this.targetTransform.position;
          print("Entry Trigger, Horizontal");
        }
        else if (inSetting == TriggerType.vertTrigger)
        {
          vCam.getShiftTargetVector(verticalTarget);
          vCam.ShiftVert();
          vCam.entryTriggerTarget = this.targetTransform.position;
          print("Entry Trigger, Vertical");
        }
      }

      if (triggerState == TriggerState.outState)
      {
        if (outSetting == TriggerType.stageTrigger)
        {
          vCam.ShiftStage();
          print("Entry Trigger, ShiftStage");
        }
        else if (outSetting == TriggerType.horzTrigger)
        {
          vCam.getShiftTargetVector(horizontalTarget);
          vCam.ShiftHorz();
          vCam.entryTriggerTarget = this.targetTransform.position;
          print("Entry Trigger, Horizontal");
        }
        else if (outSetting == TriggerType.vertTrigger)
        {
          vCam.getShiftTargetVector(verticalTarget);
          vCam.ShiftVert();
          vCam.entryTriggerTarget = this.targetTransform.position;
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
    }
    else
    {
      triggerState = TriggerState.inState;
      print("init state");
    }
  }
}
