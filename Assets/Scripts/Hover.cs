using UnityEngine;

public class Hover : MonoBehaviour
{
    public float xDash = 3.5f;
    public float yDash = 1.5f;
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector = new Vector3(0, .9f, 0); //direction of movement
    public float period = 2f;
    [Range (0,1)] public float movementFactor;
    [Range(-10f, 2f)] public float gravity = -5f;
    [Range(0, 5)] public float aDrag;
    public float spikeSpeed;
    public int spikeChargeDuration;


    Vector3 blinkDirection;

    //public Transform hoverMenu;
    public GameObject hoverMenu;
    public GameObject spikePellet;
    GameObject spikeObject;
    Rigidbody rB;
    Rigidbody rBSpike;

    bool hoverOn;
    bool spikeCharging;
    bool scooting = false;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        Instantiate(hoverMenu);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hoverOn = true;
            Hovering();
            if (Input.GetKey(KeyCode.F))
            {
                hoverMenu.SetActive(true);
            }
            else
            {
                hoverMenu.SetActive(false);
            }
        }
        else
        {
            rB.isKinematic = false;
            rB.drag = 0f;
            float spikeArc = -1 + 0.7f;

            // SPIKE SHOOTER
            // spikeArc = SpikeShooter(spikeArc);

            if (Input.GetKeyDown(KeyCode.D))
            {
                //rB.isKinematic = true;

                //blinkDirection = transform.position + new Vector3(100f, 0f,0f);
                //rB.transform.position = (Vector3.Lerp(transform.localPosition, blinkDirection, Time.deltaTime));
                rB.velocity = new Vector3(xDash, yDash, 0f);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //rB.isKinematic = true;

                //blinkDirection = transform.position + new Vector3(-100f, 0f, 0f);
                //transform.position = Vector3.Lerp(transform.localPosition, blinkDirection, Time.deltaTime);

                rB.velocity = new Vector3(-xDash, 1.5f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //rB.AddRelativeForce(Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }

            if (Input.GetKey(KeyCode.Space))
            {
                scooting = false;
            }

            if (Input.GetKey(KeyCode.W))
            {
                rB.drag = aDrag - Time.deltaTime;
                Mathf.Clamp(rB.drag, 0, aDrag);
                //scooting = false;
                //rB.isKinematic = false;
                //print("Drag = " + rB.drag);

                if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
                {
                    //rB.drag = 0;
                    scooting = true;
                    //print("Drag = " + rB.drag);

                }
                else
                {
                    scooting = false;
                }
            }

            if (scooting == false)
            {
                Physics.gravity = new Vector3(0, gravity, 0);
                //print("Gravity = " + Physics.gravity);
            }
            if (scooting == true)
            {
                Physics.gravity = new Vector3(0, 0, 0);
                //print("Gravity = " + Physics.gravity);
            }

            else
            {
                scooting = false;
                return;
            }

        }

    }

    private float SpikeShooter(float spikeArc)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i <= spikeChargeDuration; i++, spikeArc++)
            {
                if (i <= spikeChargeDuration)
                {
                    GameObject spikeObject = Instantiate(spikePellet, transform.position + new Vector3(1f, spikeArc, 0f), rB.rotation);
                    rBSpike = spikeObject.GetComponent<Rigidbody>();
                    float spikeVelY = i + -1f;
                    float spikeVelX = i + 2.5f;
                    rBSpike.velocity = new Vector3(spikeVelX, spikeVelY, 0f) * spikeSpeed;
                    Destroy(spikeObject, 2f);
                    //print("fire");
                }
            }

            print("pewpewpewpewpew");

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i <= spikeChargeDuration; i++, spikeArc++)
            {
                if (i <= spikeChargeDuration)
                {
                    GameObject spikeObject = Instantiate(spikePellet, transform.position + new Vector3(-1f, spikeArc, 0f), rB.rotation);
                    rBSpike = spikeObject.GetComponent<Rigidbody>();
                    float spikeVelY = i + -1f;
                    float spikeVelX = i + 2.5f;
                    rBSpike.velocity = new Vector3(-spikeVelX, spikeVelY, 0f) * spikeSpeed;
                    Destroy(spikeObject, 2f);
                }
            }
        }

        return spikeArc;
    }

    private void Hovering()
    {
        float cycle = Time.time / period;
        float tau = Mathf.PI * 2f;
        float rawOsc = Mathf.Sin(cycle * tau);
        movementFactor = rawOsc / 1f + 0f;
        Vector3 offset = movementFactor * movementVector;
        startingPos = transform.localPosition;

        transform.position = startingPos + offset;
        rB.isKinematic = true;
    }

    private void spikeCharge()
    {
        if (spikeCharging == true)
        {
            ;
        }
        else
        {
            return;
        }
    }
}
