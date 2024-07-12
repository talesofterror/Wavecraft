using UnityEngine;


/*
 * TO DO: 
 * 
 * Refactor: Lots of issues
 * 
 * Add an object to act as the y position target for each trigger object
 * 
 * VARIABLES THAT DO NOTHING: 
 * - m_fieldOfView
 * - yPos
 * - xPosOffset
 * - horzMidOffset
 * 
 * How do I get the value of the target position from the ViewShift into this script? 
 * 
 * Instead of getting the target location from the Rocket script I should try getting it from
 * the trigger object itself. Send a message from ViewShift to Viewer (which is already happening)
 * to specify the y location
 * 
 */
public enum ViewState
{
  stageView,
  downAnim,
  vertMiddle,
  stageAnim,
  horzAnim,
  horzMiddle,
  vertAnim
}

public class ViewerOld : MonoBehaviour
{
  Camera mainCam;
  Camera overlayCam;
  Vector3 stageVector;
  Transform swivelTarget;

  [Header("Target")]
  public GameObject playerObject;

  LayerMask sensorLayer;
  public RaycastHit rayhit;

  [Header("Entry Trigger Target")]
  public Vector3 entryTriggerTarget;

  [Header("Camera Settings")]
  public float m_fieldOfView = 28f;
  public float distance = -26.8f;
  public float xPos = 0f;
  public float yPos = 0f;
  public float xPosOffset = 0f;
  public float yPosOffset = 0f;
  public float xRot = 0f;
  public float yRot = 0f;
  public float zRot = 0f;
  public float panSpeed = 2f;
  public float vertMidOffset = 6f;
  public float horzMidOffset = 6f;

  [Header("View State")]
  public ViewState state = ViewState.stageView;
  // CamState state = new CamState(CamState.current.stageView);
  public bool swivelOn = true;

  void Awake()
  {
    playerObject = GameObject.FindGameObjectWithTag("Player");
    mainCam = GetComponent<Camera>();
    overlayCam = GetComponentInChildren<Camera>();
    sensorLayer = 1 << 6;
    Cursor.visible = false;

  }

  private Quaternion initialRotation;
  void Start()
  {
    transform.position = new Vector3(xPos, playerObject.transform.position.y + yPosOffset, distance);
    stageVector = transform.position;
    initialRotation = transform.rotation;
  }

  public void ShiftDown()
  {
    state = ViewState.downAnim;
    mainCam.fieldOfView = 44f;
    overlayCam.fieldOfView = 44f;
    distance = -22f;
  }

  public void ShiftStage()
  {
    state = ViewState.stageAnim;
    mainCam.fieldOfView = 28f;
    overlayCam.fieldOfView = 28f;
    distance = -26.8f;
  }

  public void ShiftHorz()
  {
    state = ViewState.horzAnim;
    mainCam.fieldOfView = 44f;
  }

  public void ShiftVert()
  {
    state = ViewState.vertAnim;
    mainCam.fieldOfView = 44f;
  }

  private Vector3 target;
  public void getShiftTargetVector(Vector3 v)
  {
    target = v;
  }

  private void SwivelToggle()
  {
    if (swivelOn == true)
    {
      swivelOn = false;
    }
    else
    {
      swivelOn = true;
    }
  }

  public void downAnimPlay()
  {
    yPosOffset = 5;

    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(xPos, playerObject.transform.position.y - yPosOffset + vertMidOffset, distance);
    // SwivelToggle();

    transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
    // if (transform.position == endPos)
    // {
    //   state = ViewState.vertMiddle;
    // }
  }

  public void stageAnimPlay()
  {
    //yPosOffset = 4.51f;
    Vector3 startPos = transform.position;
    Vector3 endPos = stageVector;

    transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);

    state = ViewState.stageView;

  }

  public void horzAnimPlay()
  {

    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(playerObject.transform.position.x - xPosOffset + horzMidOffset, target.y, distance);
    swivelTarget = playerObject.transform;
    // transform.LookAt(swivelTarget);

    transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
    // if (transform.position == endPos)
    // {
    //   state = ViewState.horzMiddle;
    // }
  }

  public void vertAnimPlay()
  {
    yPos = 0f;
    Vector3 startPos = transform.position;
    Vector3 endPos = new Vector3(target.x, playerObject.transform.position.y - yPosOffset + vertMidOffset, distance);
    swivelTarget = playerObject.transform;
    // transform.LookAt(swivelTarget);

    transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * panSpeed);
    // if (transform.position == endPos)
    // {
    //   state = ViewState.vertMiddle;
    // }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      SwivelToggle();
    }
    if (swivelOn == true)
    {
      swivelTarget = playerObject.transform;
      transform.LookAt(swivelTarget);
    }
    if (swivelOn == false) {
      transform.rotation = initialRotation;
    }

    if (state == ViewState.stageView)
    {
      transform.position = new Vector3(xPos, playerObject.transform.position.y + yPosOffset, distance);
      transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }
    if (state == ViewState.downAnim)
    {
      downAnimPlay();
    }

    if (state == ViewState.vertMiddle)
    {
      transform.position = new Vector3(xPos, playerObject.transform.position.y - yPosOffset + vertMidOffset, distance);
      transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    if (state == ViewState.stageAnim)
    {
      stageAnimPlay();
    }

    if (state == ViewState.horzAnim)
    {
      horzAnimPlay();
    }

    if (state == ViewState.vertAnim)
    {
      vertAnimPlay();
    }

    if (state == ViewState.horzMiddle)
    {
      transform.position = new Vector3(playerObject.transform.position.x - xPosOffset + vertMidOffset, yPos, distance);
      transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }

  }
}
