using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewerObject
{
  public Vector3 position;
  public Quaternion rotation; // & set with Quaternion.Euler(f, f, f)
  public float fieldOfView;
  public enum FollowState
  {
    Stationary,
    Horizontal,
    Vertical,
    Total
  }

  public FollowState followState = new FollowState();

  public ViewerObject(
    Vector3 pos,
    Quaternion rot,
    float fov)
  {
    position = pos;
    rotation = rot;
    fieldOfView = fov;
  }

  public void setFollowState(string state)
  {
    if (state == "stationary")
    {
      followState = FollowState.Stationary;
    }
    if (state == "vertical")
    {
      followState = FollowState.Vertical;
    }
    if (state == "horizontal")
    {
      followState = FollowState.Horizontal;
    }
    if (state == "total")
    {
      followState = FollowState.Total;
    }
  }

}
