using UnityEngine;

[System.Serializable]
public class ProgressToken
{
  public string name;
  public ProgressTokenType tokenType;
  public GameObject reference;
  public int referenceNumber;
  [HideInInspector] public EnemyDamage enemy;
  [HideInInspector] public EnemyDamage[] enemyArray;
  public int dialogueIndex;
  public bool activated = false;
  public bool complete = false;

    public void setTokenType(ProgressTokenType type)
    {
      if (this.tokenType == ProgressTokenType.Enemy)
      {
        enemy = reference.GetComponent<EnemyDamage>();
      }
    }
    
    public bool getTokenStatus()
    {
      if (this.tokenType == ProgressTokenType.Enemy)
      {
        return enemy.dead;
      }
      else if (this.tokenType == ProgressTokenType.EnemyGroup)
      {
        int i;
        for (i = 0; i < enemyArray.Length; i++)
        {
          if (!enemyArray[i].dead) { continue; }
        }
        if (i == enemyArray.Length - 1) { return false; }
        else return true;
      }
      else if (this.tokenType == ProgressTokenType.numberThreshhold)
      {
        if (PLAYERSingleton.i.playerStats.data == referenceNumber)
        {
          return true;
        }
        else return false;
      }
      else return false;
    }
}

public enum ProgressTokenType
{
  Enemy,
  EnemyGroup,
  numberThreshhold,
  EntryTrigger
}
