using System.Data.Common;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  public float health;
  public float sprint;
  [HideInInspector] public float data;

  public void incrementData () {
    data++;
  }
}
