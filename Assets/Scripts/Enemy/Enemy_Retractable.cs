using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Enemy_Retractable : MonoBehaviour
{

  [SerializeField] private float speed;
  [SerializeField] private float depth;
  private enum Axis {
    x, y, z
  }
  [SerializeField] private Axis axis = Axis.y;
  private enum RetractState {
    extended, 
    retracted
  }
  [SerializeField] private RetractState retractState = RetractState.extended;
  
  [SerializeField] private bool delayed = false;

  [Header("Delayed Settings")]
  [SerializeField] private float retractedTimeDuration;
  [SerializeField] private float extendedTimeDuration;

  private Vector3 initialPosition;
  private Vector3 retractedPosition;
  private bool retracting = true;
  public bool previewPosition;

  void Awake()
    {
      initialPosition = transform.position;
      intialize();
    }

    void Start()
    {
      runRetractable();
    }

  void intialize() {
    if (axis == Axis.x) {
      retractedPosition = new Vector3(initialPosition.x + depth, initialPosition.y, initialPosition.z);
    } else if (axis == Axis.y) {
      retractedPosition = new Vector3(initialPosition.x, initialPosition.y + depth, initialPosition.z);
    } else {
      retractedPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + depth);
    }

    if (retractState == RetractState.retracted) {
      transform.position = retractedPosition;
    } else if (retractState == RetractState.extended) {
      transform.position = initialPosition;
    }
  }

  void runRetractable() {
    Debug.Log("runRetractable called");
    StartCoroutine(Movement());
  }

  void toggleRetracted () {
    retractState = retractState == RetractState.extended ? retractState = RetractState.retracted 
    : retractState = RetractState.extended;
    // Debug.Log("Movement coroutine switch");
    StopCoroutine(Movement());
    StartCoroutine(Movement());
  }

  IEnumerator Movement () {
    // Debug.Log("Movement coroutine called");
    if (retractState == RetractState.retracted) {
      for (float i = 0; transform.position != initialPosition; i += Time.deltaTime * Mathf.Abs(speed)) {
        transform.position = Vector3.Lerp(retractedPosition, initialPosition, i);
        yield return null;
      }
      toggleRetracted();
    }
    else if (retractState == RetractState.extended) {
      for (float i = 0; transform.position != retractedPosition; i += Time.deltaTime * Mathf.Abs(speed)) {
        transform.position = Vector3.Lerp(initialPosition, retractedPosition, i);
        yield return null;
      }
      toggleRetracted();
    }
  }
  
}
