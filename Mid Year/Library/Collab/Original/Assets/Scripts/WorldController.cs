using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public Light sun;
    public bool nightTime = true;
    public float daySpeed = .01f;

    public GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dayNightCycle();
        spawnZombies();
    }

    //.6 degree of rotation a second
    void dayNightCycle()
    {
        if(sun.transform.rotation.x >= 360)
        {
            sun.transform.rotation = new Quaternion(0f, sun.transform.rotation.y, sun.transform.rotation.z, sun.transform.rotation.w);

        }
        sun.transform.rotation = new Quaternion(sun.transform.rotation.x + daySpeed, sun.transform.rotation.y, sun.transform.rotation.z, sun.transform.rotation.w);

        if(sun.transform.rotation.x < 0)
        {
            nightTime = false;
        }

        else
        {
            nightTime = true;
        }
    }

    void spawnZombies()
    {
        if (nightTime)
        {
            if (Random.Range(1, 100) == 50)
            {
                //Spawn zombies
            }
        }

        else
        {
            if (Random.Range(1, 1000) == 50)
            {
                //Spawn zombies
            }
        }

    }
}
