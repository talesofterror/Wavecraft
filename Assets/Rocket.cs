using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] public float horzThrust = 3f;
    [SerializeField] public float thrustPower = 100f;
    [SerializeField] public float rcsThrust = 100f;
    [SerializeField] public float lateralThrust = 10f;
    [Range(-10f, 2f)] public float gravity;
    public float shrinkRate = .3f;

    public float touchExplosion = 5000;

    Rigidbody rigidBody;
    AudioSource rocketSound;
    Transform rBTransform;
    GameObject guyObject;
    Vector3 scaleChange;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
        rigidBody.freezeRotation = true;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ;
        rigidBody.inertiaTensorRotation = Quaternion.identity;
        rBTransform = GetComponent<Transform>();
        guyObject = GameObject.Find("Player");
        Gravity();


    }

    // Update is called once per frame
    void Update()
    {
        SoundControl();
        Thrust();
        Rotate();
    }


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                transform.Rotate(Vector3.up * touchExplosion);
                print("Stuck by enemy!");
                break;
            case "Friendly":
                print("Friendly contact.");
                break;
            case "Finish":
                print("Finish!");
                break;
            default:
                print("Dead.");
                break;
        }

    }

    private void Gravity()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
    private void SoundControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketSound.UnPause();
        }
        else
        {
            rocketSound.Pause();
        }

    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = thrustPower * Time.deltaTime;
 
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
 
            // rocketSound.UnPause();    ---- Did not work. 
        }
        else
        {

            // rocketSound.Pause();
        }
    }

    private void Rotate()
    {
        float horizontalPosition = horzThrust * Time.deltaTime;
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        float yRotate = lateralThrust * Time.deltaTime;

        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * horizontalPosition);
            transform.Rotate(Vector3.forward * rotationThisFrame*2);
            // transform.Rotate(Vector3.up * yRotate);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * horizontalPosition);
            transform.Rotate(Vector3.back * rotationThisFrame*2);
            // transform.Rotate(Vector3.up * -yRotate);

        }

        rigidBody.freezeRotation = false;

    }

}


// Side to side movement. transform for the sake of posterity. 
//
// if (Input.GetKey(KeyCode.LeftArrow))
// {
//     transform.position += (Vector3.left * Time.deltaTime);
// }