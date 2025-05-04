using System.Collections;
using UnityEngine;

public class HELPER_PlayParticleSystem : MonoBehaviour
{

  ParticleSystem pSystem;
  Transform parent;

  void Start()
  {
    pSystem = GetComponent<ParticleSystem>();
    Invoke("disappear", 0.5f);
  }

  void disappear() {
    Destroy(gameObject);
  }



}
