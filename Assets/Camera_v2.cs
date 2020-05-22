using System.Reflection;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Camera_v2 : MonoBehaviour
{
    Rigidbody camBody;
    GameObject rocketShip;
    Rocket rocketScript;
    Transform camObject;

    void Start()
    {
        camBody = GetComponent<Rigidbody>();
        rocketShip = GameObject.Find("RocketShip_v2");
        rocketScript = rocketShip.GetComponent<Rocket>();
        camObject = GetComponent<Transform>();

        RocketDebug();
        DebugListComponents();
    }

    void DebugListComponents()
    {
        Component[] cs = rocketShip.GetComponents<Component>();
        foreach (Component c in cs)
        {
            Debug.Log("@@ " + rocketShip.name + "\t[" + c.name + "] " + "\t" + c.GetType() + "\t" + c.GetType().BaseType);
            foreach(FieldInfo fi in c.GetType().GetFields())
            {
                System.Object obj = (System.Object)c;
                Debug.Log("field name: " + fi.Name + " Value: " + fi.GetValue(obj));
            }
        }
    }

    void RocketDebug()
    {
        if (rocketScript == null)
        {
            print("Nothing there, bro.");
        }
        else
        {
            print("Everything seems to be in order.");
        }
    }

    void Update()
    {
        // float thrustThisFrame = rocketScript.thrustPower * Time.deltaTime;

        // if (Input.GetKey(KeyCode.Space))
        // {
        //    camBody.isKinematic = false;
        //    camBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        // }
        // else
        // {
        //    camBody.isKinematic = true;
        // }

        // float rocketPostion = rocketShip.transform.position.y;
        // camBody.MovePosition(transform.position + rocketPostion);

        // ^ none of this shit worked smh

        transform.position = new Vector3(0f, rocketShip.transform.position.y, -10f);
    }
}
