using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Rocket : MonoBehaviour
{
    [SerializeField] public float horzThrust = 3f;
    [SerializeField] public float thrustPower = 100f;
    [SerializeField] public float rcsThrust = 100f;
    [SerializeField] public float lateralThrust = 10f;
    [SerializeField] public float deathSpiral = 3f;
    [Range(-10f, 2f)] public float gravity;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip goalSound; 

    Rigidbody rigidBody;
    AudioSource audioSource;
   
    public enum  State
    {
        Existing,
        Transcending,
        Ascending
    };

    public enum  Level
    {
        Level0,
        Level1
    };

    State state = State.Existing;
    Level level;

    // Update is called once per frame
    void Update()
    {
        if (state == State.Existing)
        {
            RespondToThrust();
            Rotate();
        }

        if (state == State.Transcending)
        {
            transform.Rotate(Vector3.up * (3 * Time.time));
            rigidBody.AddForce(Vector3.back * (deathSpiral * Time.time));
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //rigidBody.freezeRotation = true;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ; //these are keeping my guy rotating in a stable fashion though I don't know how
        rigidBody.inertiaTensorRotation = Quaternion.identity; // these are keeping my guy rotating in a stable fashion though I don't know how
        Gravity();

        int debugSceneNumber = SceneManager.GetActiveScene().buildIndex;
        print(debugSceneNumber);


    }




    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Existing)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                audioSource.PlayOneShot(deathSound);
                state = State.Transcending;
                print("Stuck by enemy!");
                Invoke("LoadLevel1", 2f);
                break;
            case "Friendly":
                print("Friendly contact.");
                break;
            case "Finish":
                state = State.Ascending;
                audioSource.PlayOneShot(goalSound);
                Invoke("LoadNextLevel", 2f);
                level = Level.Level1;
                print("Finish!");
                break;
            default:
                print("8====D~~~");
                break;
        }

    }

    private void LoadLevel0()
    {
        level = Level.Level0;
        SceneManager.LoadScene(2);
    }

    private void LoadLevel1()
    {
        level = Level.Level1;
        SceneManager.LoadScene(1); //to do, allow for more levels 
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentSceneIndex + 1;
        if (currentSceneIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
        print(currentSceneIndex);
    }

    private void Gravity()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = thrustPower * Time.deltaTime;

        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        audioSource.PlayOneShot(thrustSound);
        // rocketSound.UnPause();    ---- Did not work. 
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