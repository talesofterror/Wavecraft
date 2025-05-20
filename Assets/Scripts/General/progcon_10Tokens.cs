using UnityEngine;
using UnityEngine.AI;

public class progcon_NData : MonoBehaviour
{
  public int numberOfData;
  [HideInInspector] public ProgressCondition<bool> nData;
  [SerializeField] int dialogueSetIndex;

  void Start()
  {
    nData = new ProgressCondition<bool>();
  }

  bool checkCondition()
  {
    nData.setCondition(PLAYERSingleton.i.playerStats.data == numberOfData);
    return nData.conditionItem;
  }
}
