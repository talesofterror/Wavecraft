using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialControl1 : MonoBehaviour
{

  Material material;
  GameObject player;

  void Start()
  {
    material = GetComponent<Renderer>().material;
    player = GameObject.FindGameObjectWithTag("GuyBase");
  }

  void Update()
  {
    material.SetFloat("_Player_Position_X", player.transform.position.x);
    material.SetFloat("_Player_Position_Y", player.transform.position.y);
  }
}
