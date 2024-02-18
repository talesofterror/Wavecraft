using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float hP = 100;
    public float baseHP;
    public float aP = 100;
    public float baseAP;

    void Start () {
      baseHP = hP;
      baseAP = aP;
    }
}
