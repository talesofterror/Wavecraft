using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHP = 100;
    public float enemyAP = 100;
    public GameObject damager;
    AmmoCollisions damagerStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter (Collision collision) {
      damager = collision.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // void adjustCurrentHP (float damage) {
    //   enemyHP -= damage;
    // }
}
