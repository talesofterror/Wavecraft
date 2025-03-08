using UnityEngine;

public class Hover : MonoBehaviour
{
    Rigidbody rB;
    public float xDash = 3.5f;
    public float yDash = 1.5f;
    public float drag;

    public GameObject hoverMenu;
    public GameObject spikePellet;

    bool scooting = false;
    [Range(-10f, 2f)] public float gravity = -5f;

    void Start()
    {
        rB = GetComponent<Rigidbody>();
        Instantiate(hoverMenu);
    }

    void Update()
    {

        if (Input.GetMouseButton(0)){
          
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // hoverOn = true;
            // Hovering();
            // if (Input.GetKey(KeyCode.F))
            // {
            //     hoverMenu.SetActive(true);
            // }
            // else
            // {
            //     hoverMenu.SetActive(false);
            // }
        }
        else
        {
            rB.isKinematic = false;
            rB.linearDamping = 0f;

            if (Input.GetKeyDown(KeyCode.E) || Input.GetAxis("DPad-Horizontal") > 0)
            {
                rB.linearVelocity = new Vector3(xDash, yDash, 0f);
            }

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("DPad-Horizontal") < 0)
            {
                rB.linearVelocity = new Vector3(-xDash, yDash, 0f);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < -0.25f || Input.GetAxis("DPad-Vertical") < -0.50f)
            {
                rB.linearVelocity = new Vector3(0f, -xDash, 0f);
            }

            if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
            {
                scooting = false;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetButton("Circle") || Input.GetButton("Fire2") )
            {
                rB.linearDamping = drag - Time.deltaTime;

                if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
                {
                    scooting = true;
                }

                else
                {
                    scooting = false;
                }
            }

            if (scooting == false)
            {
                Physics.gravity = new Vector3(0, gravity, 0);
            }
            if (scooting == true)
            {
                Physics.gravity = new Vector3(0, 0, 0);
            }
        }

    }
}
