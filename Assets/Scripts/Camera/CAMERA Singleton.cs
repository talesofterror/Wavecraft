using UnityEngine;
using UnityEngine.InputSystem.UI;

public class CAMERASingleton : MonoBehaviour
{

    private static CAMERASingleton _cameraSingleton;
    public static CAMERASingleton cameraSingleton {get {return _cameraSingleton;} }

    public UICursor uICursor;
    public VirtualMouseInput virtualMouse;
    public View viewerScript;
    public PointerSensorManager pointerSensor;
    public LookAtGimbal LookAtGimbal;

    void Awake () {
      if (_cameraSingleton != null && _cameraSingleton != this) {
        Destroy(this);
      } else {
        _cameraSingleton = this;
        DontDestroyOnLoad(this);
      }

      pointerSensor.gameObject.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
