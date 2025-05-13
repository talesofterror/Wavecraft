using UnityEngine;

public class PillarbiterAnimations : MonoBehaviour
{

  Animator animator;
  Enemy_Retractable retractorScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      animator = GetComponentInChildren<Animator>();
      retractorScript = GetComponent<Enemy_Retractable>();
    }

    // Update is called once per frame
    void Update()
    {
      if (retractorScript.retractState == Enemy_Retractable.RetractState.extending) {
        animator.SetBool("Chomping", true);
      }
      if (retractorScript.retractState == Enemy_Retractable.RetractState.retracting) {
        animator.SetBool("Chomping", false);
      }
    }
}
