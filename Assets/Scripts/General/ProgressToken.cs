using UnityEngine;

[System.Serializable]
public class ProgressToken
{
  public string name;
  public ProgressTokenType tokenType;
  public GameObject referenceObject;
  public int referenceNumber;
  [HideInInspector] public EnemyDamage enemy;
  [HideInInspector] public EnemyDamage[] enemyArray;
  public bool activated = false;
  public bool complete = false;

    public void setTokenType()
    {
      if (this.tokenType == ProgressTokenType.Enemy)
      {
        enemy = referenceObject.GetComponent<EnemyDamage>();
      }
    }
    
    public bool getTokenStatus()
    {
      if (this.tokenType == ProgressTokenType.Enemy)
      {
        if (enemy.dead)
        {
          return true;
        }
        else return false;
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
        if (PLAYERSingleton.i.playerStats.data >= referenceNumber)
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
