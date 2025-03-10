using UnityEngine;

public class UICursor : MonoBehaviour
{
  void Start()
  {
      
  }

  void Update()
  {
      
  }

  public void UpdateAppearance (WORLDInteractable interactable) {
    print("UICursor: UpdateAppearance() called");
    print("UISingleton cursorTarget = " + UISingleton.uiSingleton.cursorTarget);
  }

}
