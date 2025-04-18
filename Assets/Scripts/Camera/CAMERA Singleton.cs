using UnityEngine;

public class CAMERASingleton : MonoBehaviour
{

    private static CAMERASingleton _cameraSingleton;
    public static CAMERASingleton cameraSingleton {get {return _cameraSingleton;} }

    public UICursor uICursor;
    public ViewerRevised viewerScript;
    public PointerSensorManager pointerSensor;

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
