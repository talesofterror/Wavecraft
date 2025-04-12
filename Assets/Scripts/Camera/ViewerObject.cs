using UnityEngine;

// & contains ViewObject + FollowState Enum

public class ViewerObject
{
  public Vector3 position;
  public Quaternion rotation; // ? set with Quaternion.Euler(f, f, f)
  public float fieldOfView;
  public Vector3 offsets;
  public FollowState followState;
  public bool lookAt = false;
  public bool swivel = false;

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
  public void setOffsets(float x, float y, float z)
  {
    offsets = new Vector3(x, y, z);
  }

}

public enum FollowState
{
  Stationary,
  Horizontal,
  Vertical,
  Total
}