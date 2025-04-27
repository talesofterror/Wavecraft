using UnityEngine;

public class LookAtGimbal : MonoBehaviour
{
  [HideInInspector]
  public Quaternion rotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.rotation;
    }
}
