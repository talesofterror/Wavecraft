using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    float delta = Time.deltaTime;
    Rocket rocketScript;
    GameObject rocketShip;
    GameObject cam;
    Rigidbody camBody;
    Transform rocketTransform;
    Transform camTransform;
    [SerializeField] public float thrustPower = 100f;

    // Start is called before the first frame update
    void Start()
    {
        //   rocketScript = GameObject.FindGameObjectsWithTag("Player");
        rocketScript = rocketShip.GetComponent<Rocket>();
        rocketTransform = rocketShip.GetComponent<Transform>();
        camTransform = GetComponent<Transform>();
        camBody = GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera");
    }


    void FreezeRotation()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = thrustPower * Time.deltaTime;

            camBody.AddRelativeForce(Vector3.up * thrustThisFrame);

        }
    }

}
