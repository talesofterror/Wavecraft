using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeCollision : MonoBehaviour
{
    public float rateOfScale = 2f;
    bool isShrunk = false;
    bool isShrinking = false;
    bool isExpanding = false;

    float timer = 0f;
    float lerpSmallTime = 0f;
    float lerpBigTime = 0f;
    
    Vector3 scaleChange;
    Vector3 fullScale;

    Collider colliderToggle;
    

    void Start()
    {
        scaleChange = new Vector3(0.05f, 0.05f, 0.05f);

        fullScale = this.transform.localScale;

       colliderToggle = this.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isShrunk == true)
        {
            return;
        }
        if (isShrunk == false)
        {
            isShrinking = true;
        }
    }


    void Update()
    {

        // IS SHRINKING

        if (this.isShrinking == true)
        {
            StartCoroutine(IEShrinker(fullScale, scaleChange));
        }
        if (isShrinking == false)
        {
            lerpSmallTime = 0f;
        }

        // IS SHRUNK

        if (isShrunk == true)
        {
            StartCoroutine(ShrunkTimer());
        }
        if (this.isShrunk == false)
        {
            
            timer = 0f;
        }

        // IS EXPANDING

        if (isExpanding == true)
        {
            StartCoroutine(IEExpander(scaleChange, fullScale));
        }
        if (isExpanding == false)
        {
            lerpBigTime = 0f;
        }


        Debug();
        

        // modulo ticks like a clock. 
    }




    IEnumerator IEShrinker(Vector3 a, Vector3 b)
    {
        lerpSmallTime += Time.deltaTime * 1.2f;

        if (lerpSmallTime < 1)
        {
            transform.localScale = Vector3.Lerp(a, b, lerpSmallTime);
            colliderToggle.enabled = false;
            //print("Lerp Shrink Timer = " + lerpSmallTime);

        }
        if (lerpSmallTime > 1)
        {
            transform.localScale = b;
            isShrinking = false;
            isShrunk = true;
            yield return null;
        }
    }


    IEnumerator ShrunkTimer()
    {
        timer += Time.deltaTime * 5.1f;

        if (timer < 30)
        {
            isShrinking = false;
            //isShrunk = true;
            colliderToggle.enabled = false;
            //print("Timer = " + timer);
        }
        if (timer > 30)
        {
            isShrunk = false;
            isExpanding = true;
            yield return null;
        }
    }

    IEnumerator IEExpander(Vector3 a, Vector3 b)
    {
        lerpBigTime += Time.deltaTime * 1.2f;

        if (lerpBigTime < 1)
        {
            transform.localScale = Vector3.Lerp(a, b, lerpBigTime);
            //print("Lerp Expand Timer = " + lerpBigTime);

        }
        if (lerpBigTime > 1)
        {
            transform.localScale = b;
            isExpanding = false;
            isShrunk = false;
            colliderToggle.enabled = true;
            yield return null;
        }
    }


    void Debug()
    {


        //print("Time = " + (int)Time.time);
        //print("Unscaled Time = " + (int)Time.unscaledTime);
        //print("Fixed Time = " + (int)Time.fixedTime);
        //print("Time Scale = " + (int)Time.timeScale);
        //print("Unscaled Time / 2.5f % 2) = " + (int)(Time.unscaledTime / 2.5f % 3.1));
        //print("Unscaled Time / 2.5f = " + (int)(Time.unscaledTime / 1.5f));
        //print("Collider Enabled? = " + colliderToggle.enabled);
    }


}
