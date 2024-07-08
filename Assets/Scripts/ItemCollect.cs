using System.Collections;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
  public GameObject floppyMesh;
  public ParticleSystem pSystem;
  Vector3 initialScale;
  Vector3 targetScaleVector;
  void Start()
  {
    if (pSystem)
    {
      pSystem.gameObject.SetActive(false);
    }
    initialScale = floppyMesh.transform.localScale;
    targetScaleVector = new Vector3(0, 0, 0);
  }

  void OnTriggerEnter(Collider trigger)
  {
    StartCoroutine(ScaleDown());
    if (pSystem)
    {
      pSystem.gameObject.SetActive(true);
      pSystem.Play();
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
    Destroy(gameObject);
  }
  void Update()
  {

  }
}
