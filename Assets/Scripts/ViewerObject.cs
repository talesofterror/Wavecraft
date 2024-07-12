using UnityEngine;

// & contains ViewObject + FollowState Enum

public class ViewerObject
{
  public Vector3 position;
  public Quaternion rotation; // ? set with Quaternion.Euler(f, f, f)
  public float fieldOfView;

  public FollowState followState = new FollowState();

  public bool lookAt = false;

  public ViewerObject(
    Vector3 pos,
    Quaternion rot,
    float fov)
  {
    position = pos;
    rotation = rot;
    fieldOfView = fov;
  }

  public void setFollowState(FollowState state)
  {
    followState = state;
  }
  public void setLookAt(bool b)
  {
    lookAt = b;
  }

}

public enum FollowState
{
  Stationary,
  Horizontal,
  Vertical,
  Total
}