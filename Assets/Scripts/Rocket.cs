using UnityEngine.SceneManagement;
using UnityEngine;


public class Rocket : MonoBehaviour
{
  [SerializeField] public float thrustPower = 600f;
  [SerializeField] public float rotationSpeed = 50f;
  [SerializeField] public float deathSpiral = 3f;
  [SerializeField] public float axisRotationFactor = 0;
  public float leftTwistRot = 3f;

  public bool enemyCollissionsOn = true;

  [SerializeField] AudioClip thrustSound;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip goalSound;

  Rigidbody rigidBody;
  AudioSource audioSource;
  public GameObject entryAxisTarget;


  public enum StateOfBeing
  {
    Existing,
    Transcending,
    Ascending
  };

  public enum Level
  {
    Level0,
    Level1
  };

  StateOfBeing state = StateOfBeing.Existing;
  Level level;



  // Update is called once per frame
  void Update()
  {

    if (state == StateOfBeing.Existing)
    {
      ThrustControls();
      Rotate();
      RotateJoystick();

    }

    if (state == StateOfBeing.Transcending)
    {
      transform.Rotate(Vector3.up * (3 * Time.time));
      rigidBody.AddForce(Vector3.back * (deathSpiral * Time.time));
      audioSource.Stop();
    }

    if (Input.GetKey(KeyCode.N))
    {
      LoadNextLevel();
    }

    PauseGame();


  }

  private void FixedUpdate()
  {
    if (state == StateOfBeing.Existing)
    {
      ThrustControls();
      Rotate();
      RotateJoystick();

    }

  }

  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
    //rigidBody.freezeRotation = true;
    // rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ; //these are keeping my guy rotating in a stable fashion though I don't know how
    rigidBody.inertiaTensorRotation = Quaternion.identity; // these are keeping my guy rotating in a stable fashion though I don't know how

    //Gravity();

    print("Scene Number:" + SceneManager.GetActiveScene().buildIndex);


  }




  void OnCollisionEnter(Collision collision)
  {

    if (state != StateOfBeing.Existing)
    {
      return;
    }

    switch (collision.gameObject.tag)
    {
      case "Enemy":
        if (enemyCollissionsOn)
        {
          audioSource.PlayOneShot(deathSound);
          state = StateOfBeing.Transcending;
          print("Stuck by enemy!");
          Invoke("LoadCurrent", 2f);
          break;
        }
        else { return; };
      case "Friendly":
        print("Friendly contact.");
        break;
      case "Finish":
        state = StateOfBeing.Ascending;
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

  public void OnTriggerEnter(Collider collision)
  {
    switch (collision.gameObject.tag)
    {
      case "EntryTrigger":

        print("Entry Trigger triggered");
        break;
    }
  }

  void LoadCurrent()
  {
    int activeScene = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(activeScene);
  }

  void LoadNextLevel()
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

  private void ThrustControls()
  {
    if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
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
  }

  private void Rotate()
  {
    float yRotate = rotationSpeed * Time.deltaTime;

    rigidBody.freezeRotation = true;

    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(Vector3.forward * yRotate);
    }

    if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(Vector3.back * yRotate);
    }

    // if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.LeftShift))
    // {
    //   rotationSpeed = rotationSpeed + 10;
    // }

    // if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.LeftShift))
    // {
    //   rotationSpeed = rotationSpeed + 10;
    // }

    // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetAxis("DPad-Horizontal") != 0)
    // {
    //   transform.eulerAngles = new Vector3(0, 0, 0);
    // }


    rigidBody.freezeRotation = false;

  }

  private void RotateJoystick()
  {
    float joystickX = Input.GetAxis("Horizontal");

    transform.Rotate(Vector3.back * joystickX);

  }


  private void PauseGame()
  {
    if (Input.GetKey(KeyCode.RightControl))
    {
      Time.timeScale = 0;
    }

    else
    {
      Time.timeScale = 1;
    }
  } //doesn't really work. one enemy builds up projectiles and shoots them all when unpaused
    // probably need a dedicated time manager, and to possible add pause state
    // definitions to each class.


}




// Side to side movement. transform for the sake of posterity. 
//
// if (Input.GetKey(KeyCode.LeftArrow))
// {
//     transform.position += (Vector3.left * Time.deltaTime);
// }