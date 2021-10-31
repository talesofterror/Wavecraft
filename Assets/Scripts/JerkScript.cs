
using UnityEngine;



/// <summary>
/// CURRENT ISSUES: 
/// 
/// The physics on the pellets needs work. I was originally relying on some kind of memory allocation issue with the big polyp wall to make the pellets
/// fall, but once I deactivate the wall the pellets shoot farther out. The only thing making their behavior random right now is their collisions with
/// each other as they are each instantiated. I will have to code in actual randomness. 
/// </summary>




public class JerkScript : MonoBehaviour
{
    Rigidbody rBPellet;

    Vector3 spawnLoc;

    public GameObject jerkPellet;
    GameObject pelletObject;
    public GameObject spawnTransform;
    public GameObject malOrb1;
    public GameObject malOrb2;

    public float pelletVelX = 5f;
    public float pelletVelY = 2f;
    public float destroyPellet = 1f;
    
    bool orbHitBool1;
    bool orbHitBool2;

    bool detectHit1 = true;
    bool detectHit2 = true;

    bool pelletFire;
    bool jerkSpitting = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Timer();

        DetectOrbHits();

        if (jerkSpitting == true)
        {
            PelletFire();
        }
        else if (jerkSpitting == false)
        {

        }
    }

    private void DetectOrbHits()
    {
        float orbHit = 0;

        if (detectHit1 == true)
        {
            orbHitBool1 = malOrb1.GetComponent<OrbCollisions>().hit;
            // malOrb1 is an object with the OrbCollisions script attached
            // that script contains a bool named "hit"
            // when that bool is true +1 is added to the "orbHit" float initiated in this function. 
            // when the orbHit float equals 2, the "jerkSpitting" bool is set to false, stopping the jerk spitting
        }

        if (detectHit2 == true)
        {
            orbHitBool2 = malOrb2.GetComponent<OrbCollisions>().hit;
        }

        if (orbHitBool1 == true)
        {
            orbHit = orbHit + 1;
            print("Orb 1 hit!");
            print("orbHit value = " + orbHit);
            detectHit1 = false;
        }

        if (orbHitBool2 == true)
        {
            orbHit++;
            print("Orb 2 hit!");
            print("orbHit value = " + orbHit);
            detectHit2 = false;
        }

        if (orbHit == 2)
        {
            jerkSpitting = false;
        }
    }

    private void Timer()
    {
        float timePassed = Time.unscaledTime / 2.5f % 2;

        // print(timePassed);
        // print(Time.time % 2);

        if (timePassed < 1)
        {
            pelletFire = true;
            //print("Jerk Spits");
        }
        if (timePassed > 1)
        {
            pelletFire = false;
        }
    }

    private void PelletFire()
    {
        if (pelletFire == true)
        {

            if (transform.position.x < 0)
            {
                pelletObject = Instantiate(jerkPellet, spawnTransform.transform.position, Quaternion.identity);
                pelletObject.transform.parent = this.transform.parent;
                rBPellet = pelletObject.GetComponent<Rigidbody>();
                rBPellet.velocity = new Vector3(pelletVelX, pelletVelY, 0);
                Destroy(pelletObject, destroyPellet);
            }
            if (transform.position.x > 0)
            {
                pelletObject = Instantiate(jerkPellet, spawnTransform.transform.position, Quaternion.identity);
                pelletObject.transform.parent = this.transform.parent;
                rBPellet = pelletObject.GetComponent<Rigidbody>();
                rBPellet.velocity = new Vector3(-pelletVelX, pelletVelY, 0);
                Destroy(pelletObject, destroyPellet);
            }
        }
    }

}
