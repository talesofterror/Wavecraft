using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArc : MonoBehaviour
{
    
    public float gizmoSize = 10 / 4;
    public float bulletAngle;
    public float radius = 10;
    public float offsetX = 0;
    public float bulletSpeed = 10;

    public GameObject spawnPoint1;
    Vector3 spawn2Vector;
    Transform spawnPoint2;

    public GameObject CrosshairMesh1;
    public GameObject CrosshairMesh2;
    public float crosshairScale = 1f;
    public float crosshairSpeed;
    GameObject cHObject1;
    GameObject cHObject2;

    public GameObject bulletLeft;
    public GameObject bulletRight;
    Rigidbody bRRB;

    float angle;
    Vector3 angledTransform1;
    Vector3 angledTransform2;
    Vector3 crosshairTransform;
    Vector3 cH1Scale;
    Vector3 cH2Scale;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {


        //spawnPoint2.transform.position = spawn2Vector;

        //cHObject1 = Instantiate(CrosshairMesh1, spawnPoint1.transform);
        //cHObject2 = Instantiate(CrosshairMesh1, spawnPoint2.transform);



        cHObject1 = Instantiate(CrosshairMesh1, spawnPoint1.transform);
        cHObject1.SetActive(true);
        cHObject1.transform.parent = transform.parent;

        cHObject2 = Instantiate(CrosshairMesh2, spawnPoint1.transform);
        cHObject2.SetActive(true);
        cHObject2.transform.parent = transform.parent;

        //cHObject1.transform.localScale = cH1Scale;
        //cHObject2.transform.localScale = cH1Scale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cH1Scale = new Vector3(crosshairScale, crosshairScale, crosshairScale);
        cHObject1.transform.localScale = cH1Scale;
        cHObject2.transform.localScale = cH1Scale;

        angle = (Mathf.PI * 2 / 1f + bulletAngle);
        float angle2 = (Mathf.PI * 2 / 1f + bulletAngle - 1.5f);
        angledTransform1 = new Vector3(transform.position.x - offsetX + (radius * Mathf.Cos(angle)), transform.position.y + (radius * Mathf.Sin(angle)), transform.position.z);
        angledTransform2 = new Vector3(transform.position.x - offsetX + (radius * Mathf.Sin(angle2)), transform.position.y + (radius * Mathf.Cos(angle2)), transform.position.z);

        cHObject1.transform.position = angledTransform1;
        spawn2Vector = new Vector3(cHObject1.transform.position.x * -2, cHObject1.transform.position.y * -2, cHObject1.transform.position.z * -2);
        cHObject2.transform.position = angledTransform2;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            bulletAngle -= 0.07f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            bulletAngle += 0.07f;
        }


        if (Input.GetKey(KeyCode.Q))
        {
            GameObject bR = Instantiate(bulletLeft);
            bR.transform.position = angledTransform2;
            bR.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bRRB = bR.GetComponent<Rigidbody>();
            bRRB.velocity = (angledTransform2 - transform.position) * bulletSpeed;
            bRRB.useGravity = false;
            Destroy(bR, 0.3f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            GameObject bR = Instantiate(bulletLeft);
            bR.transform.position = angledTransform1;
            bR.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            bRRB = bR.GetComponent<Rigidbody>();
            bRRB.velocity = (angledTransform1 - transform.position) * 10;
            bRRB.useGravity = false;
            Destroy(bR, 0.3f);
        }

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(angledTransform1, gizmoSize);
    }
}
