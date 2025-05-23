﻿using UnityEngine.SceneManagement;
using UnityEngine;


public class Rocket : MonoBehaviour
{
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

  public enum ControlScheme
  {
    oldControls,
    newControls,
    inputSystem
  };

  public bool controlsActive;

  StateOfBeing state = StateOfBeing.Existing;
  Level level;

  [Header("Controls")]
  public ControlScheme controlScheme = ControlScheme.oldControls;

  [Header("Player Control")]
  [SerializeField] public float thrustPower = 600f;
  [SerializeField] public float rotationSpeed = 50f;
  [SerializeField] public float axisRotationFactor = 0;
  public Rigidbody rigidBody;

  [Header("Physics")]
  public float gravityY = 0;

  [Header("Sound")]
  [SerializeField] AudioClip thrustSound;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip goalSound;
  AudioSource audioSource;

  [Header("Navigation")]
  public GameObject navCursor;
  public GameObject entryAxisTarget;

  [Header("Death")]
  [SerializeField] public float deathSpiral = 3f;
  public bool enemyCollissionsOn = true;



  private void FixedUpdate()
  {
    if (state == StateOfBeing.Existing && PLAYERSingleton.i.controlsActive)
    {
      if (controlScheme == ControlScheme.oldControls)
      {
        controlsOLD();
      }
      if (controlScheme == ControlScheme.newControls)
      {
        controlsNEW();
      }
      if (controlScheme == ControlScheme.inputSystem) {
        print("Using Input System");
      }
    }
  }

  void Update()
  {
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


    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.C))
    {
      toggleEnemyCollisions();
    }

  }

  void Awake()
  {
    rigidBody = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();

    rigidBody.inertiaTensorRotation = Quaternion.identity;
    // this is keeping my guy rotating in a stable fashion though I don't know how

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
  public float boost = 3;
  float boost_Live
   = 4;
  void controlsNEW()
  {
    float thrustPower_Keys = thrustPower * Time.deltaTime * boost_Live;
    Physics.gravity = new Vector3(0, gravityY, 0);
    rigidBody.linearDamping = 2f;
    rigidBody.angularDamping = 0.0f;

    if (Input.GetKey(KeyCode.LeftShift))
    {
      boost_Live = 7;
      rigidBody.linearDamping = 1f;
    }
    else
    {
      boost_Live = boost;
    }

    rigidBody.freezeRotation = true;

    if (Input.GetKey(KeyCode.A)
    || Input.GetAxis("Horizontal") < -0.25f
    || Input.GetAxis("DPad-Horizontal") < -0.50f
      )
    {
      rigidBody.AddForce(Vector3.left * thrustPower_Keys);
    }
    if (Input.GetKey(KeyCode.D)
    || Input.GetAxis("Horizontal") > 0.25f
    || Input.GetAxis("DPad-Horizontal") > 0.50f
      )
    {
      rigidBody.AddForce(Vector3.right * thrustPower_Keys);
    }
    if (Input.GetKey(KeyCode.W)
    || Input.GetAxis("Vertical") > 0.25f
    || Input.GetAxis("DPad-Vertical") > 0.50f
      )
    {
      rigidBody.AddForce(Vector3.up * thrustPower_Keys);
    }
    if (Input.GetKey(KeyCode.S)
    || Input.GetAxis("Vertical") < -0.25f
    || Input.GetAxis("DPad-Vertical") < -0.50f
      )
    {
      rigidBody.AddForce(Vector3.down * thrustPower_Keys);
    }

  }

  void controlsOLD()
  {
    if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
    {
      ApplyThrust();
    }
    else
    {
      audioSource.Stop();
    }

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
    if (Input.GetKey(KeyCode.W))
    {
      transform.Rotate(Vector3.up * yRotate);
    }

    rigidBody.freezeRotation = false;

    float joystickX = Input.GetAxis("Horizontal");
    transform.Rotate(Vector3.back * joystickX);
  }

  private void ApplyThrust()
  {
    float thrustThisFrame = thrustPower * Time.deltaTime;

    rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
    audioSource.PlayOneShot(thrustSound);
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

  private void toggleEnemyCollisions()
  {
    if (enemyCollissionsOn)
    {
      enemyCollissionsOn = false;
    }
    else
    {
      enemyCollissionsOn = true;
    }
  }
}