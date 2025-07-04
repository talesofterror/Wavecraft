using UnityEngine;

public class DataManager : MonoBehaviour
{

  ItemCollect[] DataGroup;

  public float globalRegenTimer = 2f;

  void Start()
  {
    DataGroup = GetComponentsInChildren<ItemCollect>();
    SetRegenTimer(globalRegenTimer);
  }

  void Update()
  {

  }

  public void SetRegenTimer(float timer)
  {
    for (int i = 0; i < DataGroup.Length; i++)
    {
      DataGroup[i].regenerationTimer = timer;
    }
  }
}
