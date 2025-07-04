using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;

public class PlayerControls : MonoBehaviour
{

  PlayerInput playerInput;
  [HideInInspector] public InputAction moveAction;
  [HideInInspector] public InputAction lookAction;
  [HideInInspector] public InputAction primaryAttackAction;
  [HideInInspector] public InputAction sprintAction;
  [HideInInspector] public InputAction interactAction;
  [HideInInspector] public InputAction pauseAction;

  [HideInInspector] public bool sprinting;

  Rigidbody rB;
  VirtualMouseInput virtualCursor;

  public float moveSpeed = 2f;
  public float defaultLinearDamping = 2f;
  public float defaultAngularDamping = 0f;

  public float sprintSpeed;
  public Vector3 gravity;
  [SerializeField] float sprintDamping;
  [SerializeField] float sprintAngularDamping;

  void Start()
  {
    playerInput = GetComponent<PlayerInput>();
    moveAction = playerInput.actions.FindAction("Move");
    lookAction = playerInput.actions.FindAction("Look");
    primaryAttackAction = playerInput.actions.FindAction("Primary Attack");
    sprintAction = playerInput.actions.FindAction("Sprint");
    interactAction = playerInput.actions.FindAction("Interact");
    pauseAction = playerInput.actions.FindAction("Pause");

    Physics.gravity = gravity;

    rB = PLAYERSingleton.i.rB;
    virtualCursor = CAMERASingleton.i.virtualMouse;
  }

  void Update()
  {
    if (PLAYERSingleton.i.controlsActive == true)
    {
      Movement();
      Attack();
    }

    CursorMovement();
    PauseListener();
  }

  private void Attack()
  {
    if (primaryAttackAction.WasPerformedThisFrame())
    {
      PLAYERSingleton.i.playerAttack.fireProjectile();
    }
  }

  private void CursorMovement()
  {
    Cursor.lockState = CursorLockMode.Confined;

    Vector2 currentPosition = virtualCursor.virtualMouse.position.ReadValue();
    Vector2 newPosition = currentPosition;

    newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
    newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

    InputState.Change(virtualCursor.virtualMouse.position, newPosition);
  }

  private void Movement()
  {
    rB.freezeRotation = true;
    float _moveSpeed = moveSpeed;

    if (sprintAction.IsPressed())
    {
      if (!PLAYERSingleton.i.sprintDisabled)
      {
        sprinting = true;
        _moveSpeed = sprintSpeed;
        rB.linearDamping = sprintDamping;
        rB.angularDamping = sprintAngularDamping;
        // Vector3 sprintingCounterForce = rB.linearVelocity * -5f;
        // Physics.gravity = gravity + sprintingCounterForce;
        // Physics.gravity = gravity;
      }
    }
    else
    {
      _moveSpeed = moveSpeed;
      sprinting = false;
      rB.linearDamping = defaultLinearDamping;
      rB.angularDamping = defaultAngularDamping;
      
    }

    Vector3 moveForce = new Vector3(
      moveAction.ReadValue<Vector2>().x * ((_moveSpeed * Time.deltaTime) * 100),
      moveAction.ReadValue<Vector2>().y * ((_moveSpeed * Time.deltaTime) * 100),
    0);

    // Physics.gravity = gravity;
    rB.AddForce(moveForce);

    // Debug.Log("sprinting: " + sprinting);
    
  }

  public void interactionListener()
  {
    if (interactAction.WasPressedThisFrame())
    {
      if (!GAMESingleton.i.engaged_Dialogue)
      {
        UISingleton.i.cursorTarget.dialogueTrigger.TriggerDialogue();
      }
      else
      {
        GAMESingleton.i.dialogueManager.DisplayNextSentence();
      }
    }
  }
  
  
  public void PauseListener()
  {
    if (pauseAction.WasPressedThisFrame() && !PLAYERSingleton.i.paused)
    {
      Debug.Log("pauseAction pressed, " + "Single pause state: " + !PLAYERSingleton.i.paused);
      PLAYERSingleton.i.PauseToggle("paused");
    }
    else if (pauseAction.WasPressedThisFrame() && PLAYERSingleton.i.paused)
    {
      Debug.Log("pauseAction pressed, " + "Single pause state: " + !PLAYERSingleton.i.paused);
      PLAYERSingleton.i.PauseToggle("unpaused");
    }
  }
}
