using System.Collections;
using System.Net;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
  ParticleSystem pSystem;
  Vector3 initialScale;
  Vector3 targetScaleVector;

  [SerializeField] bool regeneration = false;
  [SerializeField] float regenerationTimer = 0.5f;
  void Awake()
  {
    pSystem = GetComponent<ParticleSystem>() ? GetComponent<ParticleSystem>(): null;

    if (pSystem)
    {
      pSystem.Stop();
    }
    initialScale = transform.localScale;
    targetScaleVector = new Vector3(0, 0, 0);
  }

  void Start()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, PLAYERSingleton.i.interactionZ);
  }

  void OnTriggerEnter(Collider trigger)
  {
    if (trigger.CompareTag("GuyBase")) {
      StartCoroutine(ScaleDown());

      if (pSystem)
      {
        pSystem.Play();
      }

      if (CompareTag("Data")) {
        PLAYERSingleton.i.collectData();
      }
    }
    else if (trigger.CompareTag("Enemy")) {
      return;
    }
  }

  public float shrinkSpeed = 1.5f;
  float i;
  IEnumerator ScaleDown()
  {
    for (i = 0; i < 1; i += Time.deltaTime * shrinkSpeed)
    {
      transform.localScale = Vector3.Lerp(initialScale, targetScaleVector, i);
      yield return null;
    }

    if (regeneration) {
      Regenerate();
    } else if (!regeneration) {
      gameObject.SetActive(false);
    }
  }
  IEnumerator ScaleUp()
  {
    for (i = 0; i < 1; i += Time.deltaTime * shrinkSpeed)
    {
      transform.localScale = Vector3.Lerp(targetScaleVector, initialScale, i);
      yield return null;
    }
  }

  void Regenerate () {
    StartCoroutine(IERegen());
  }

  IEnumerator IERegen () {
    UTILITY.SetToggleRendererColider(this.gameObject);
    yield return new WaitForSeconds(0.5f);
    UTILITY.SetToggleRendererColider(this.gameObject);
    StartCoroutine(ScaleUp());
  }
}

