using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;

public class PLAYERControls : MonoBehaviour
{
  
  PlayerInput playerInput;
  [HideInInspector] public InputAction moveAction;
  [HideInInspector] public InputAction lookAction;
  [HideInInspector] public InputAction primaryAttackAction;
  [HideInInspector] public InputAction sprintAction;

  Rigidbody rB;
  VirtualMouseInput virtualCursor;

  public float moveSpeed = 2f;
  public float sprintSpeed;

  void Start()
  {
      playerInput = GetComponent<PlayerInput>();
      moveAction = playerInput.actions.FindAction("Move");
      lookAction = playerInput.actions.FindAction("Look");
      primaryAttackAction = playerInput.actions.FindAction("Primary Attack");
      sprintAction = playerInput.actions.FindAction("Sprint");

      rB = PLAYERSingleton.playerSingleton.rB;
      virtualCursor = CAMERASingleton.cameraSingleton.virtualMouse;
  }

  void Update()
  {
    Movement();
    Attack();
    CursorMovement();
  }

  private void Attack() {
    if (primaryAttackAction.WasPerformedThisFrame()) {
      PLAYERSingleton.playerSingleton.playerAttack.fireProjectile();
    }
  }

  private void CursorMovement() {
    Cursor.lockState = CursorLockMode.Confined;

    Vector2 currentPosition = virtualCursor.virtualMouse.position.ReadValue();
    Vector2 newPosition = currentPosition;

    newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
    newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

    InputState.Change(virtualCursor.virtualMouse.position, newPosition);
  }

  private void Movement()
  {
    rB.linearDamping = 2f;
    rB.angularDamping = 0.0f;
    rB.freezeRotation = true;

    float _moveSpeed = moveSpeed * Time.deltaTime;

    if (sprintAction.IsPressed()) {
      _moveSpeed = moveSpeed + sprintSpeed;
      // rB.linearDamping = 3f;
      Debug.Log("Sprint action pressed");
    } else {
      _moveSpeed = moveSpeed;
    }

    Vector3 moveForce = new Vector3(
      moveAction.ReadValue<Vector2>().x * _moveSpeed, 
      moveAction.ReadValue<Vector2>().y * _moveSpeed, 
    0);

    rB.AddForce(moveForce);

    Physics.gravity = new Vector3(0, PLAYERSingleton.playerSingleton.rocket.gravityY, 0);
    // Debug.Log("moveAction value: " + moveAction.ReadValue<Vector2>());
  }
}
