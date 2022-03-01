using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public Light sun;
    public Light moon;

    public bool nightTime;
    public float daySpeed = 0.02f;
    private float nightSpeedCooldown;
    private float previousNightRotation;
    private bool increasing;

    public GameObject house1;
    public GameObject house2;
    public GameObject house3;
    public GameObject house4;
    public GameObject house5;
    public GameObject house6;
    public GameObject house7;
    public GameObject house8;
    public GameObject house9;
    public GameObject house10;

    public List<Transform> healthSpawns;
    public List<Transform> zombieSpawns;
    public List<Transform> ammoSpawns;

    public GameObject zombie;
    public GameObject cactus;
    public GameObject health;
    public GameObject ammo;

    public int ammoAmount ;
    public int medkitAmount;

    // Start is called before the first frame update
    void Start()
    {
        healthSpawns = new List<Transform> { house1.transform.GetChild(0), house2.transform.GetChild(0), house3.transform.GetChild(0), house4.transform.GetChild(0), house5.transform.GetChild(0), house6.transform.GetChild(0), house7.transform.GetChild(0), house8.transform.GetChild(0), house9.transform.GetChild(0), house10.transform.GetChild(0)};
        zombieSpawns = new List<Transform> { house1.transform.GetChild(1), house2.transform.GetChild(1), house3.transform.GetChild(1), house4.transform.GetChild(1), house5.transform.GetChild(1), house6.transform.GetChild(1), house7.transform.GetChild(1), house8.transform.GetChild(1), house9.transform.GetChild(1), house10.transform.GetChild(1)};
        ammoSpawns = new List<Transform> { house1.transform.GetChild(2), house2.transform.GetChild(2), house3.transform.GetChild(2), house4.transform.GetChild(2), house5.transform.GetChild(2), house6.transform.GetChild(2), house7.transform.GetChild(2), house8.transform.GetChild(2), house9.transform.GetChild(2), house10.transform.GetChild(2)};
        Invoke("SpawnZombie", 2);
        Invoke("SpawnPickup", 4);
    }

    // Update is called once per frame
    void Update()
    {
        DayNightCycle();
        //SpawnZombies();
        //SpawnPickups();
    }

    void CheckIncrease()
    {
        if (Time.time > nightSpeedCooldown)
        {
            nightSpeedCooldown = Time.time + .5f;
            increasing = (Mathf.Abs(sun.transform.rotation.x) > Mathf.Abs(previousNightRotation));
            previousNightRotation = sun.transform.rotation.x; 
        }

    }

    //.6 degree of rotation a second
    void DayNightCycle()
    {
        sun.transform.Rotate(daySpeed, 0f, 0f, Space.Self);
        CheckIncrease();

        if (increasing)
        {
            nightTime = false;
            moon.shadows = LightShadows.None;

        }
        else
        {
            nightTime = true;
            moon.shadows = LightShadows.Soft;

        }
    }

    void SpawnZombie()
    {
        float randTime;
        if (nightTime)
        {
            randTime = Random.Range(1, 2);
            Instantiate(zombie, zombieSpawns[Random.Range(0, 10)]);
            Invoke("SpawnZombie", randTime);
        }
        else
        {
            randTime = Random.Range(3, 6);
            Instantiate(zombie, zombieSpawns[Random.Range(0, 10)]);
            Invoke("SpawnZombie", randTime);
        }
    }

    void SpawnZombies()
    {
        if (nightTime)
        {
            if ((int)Random.Range(1, 250) == 50)
            {
                Instantiate(zombie, zombieSpawns[Random.Range(0, 10)]);
            }
        }

        else
        {
            if ((int)Random.Range(1, 1000) == 50)
            {
                Instantiate(zombie, zombieSpawns[Random.Range(0, 10)]);
                Debug.Log("zombie spawned");
            }
        }

    }
    public void TakenMed()
    {
        medkitAmount--;
    }
    public void TakenAmmo()
    {
        ammoAmount--;
    }

    void SpawnPickup()
    {
        float randTime = Random.Range(4, 8);
        if (!nightTime)
        {
            if (medkitAmount <= 8 && Random.Range(1, 4) <= 2)
            {
                Instantiate(health, healthSpawns[Random.Range(0, 10)]);
                Debug.Log("health spawned");
                medkitAmount++;
            }
            if(ammoAmount <= 8 && Random.Range(1, 3) <= 2)
            {
                Instantiate(ammo, ammoSpawns[Random.Range(0, 10)]);
                Debug.Log("ammo spawned");
                ammoAmount++;
            }
        }
        else
        {
            if (medkitAmount <= 8 && Random.Range(1, 6) <= 2)
            {
                Instantiate(health, healthSpawns[Random.Range(0, 10)]);
                Debug.Log("health spawned");
                medkitAmount++;
            }
            if (ammoAmount <= 8 && Random.Range(1, 5) <= 2)
            {
                Instantiate(ammo, ammoSpawns[Random.Range(0, 10)]);
                Debug.Log("ammo spawned");
                ammoAmount++;
            }
        }
        Invoke("SpawnPickup", randTime);
    }

    void SpawnPickups()
    {
        if (!nightTime)
        {
            if ((int)(Random.Range(1, 500)) == 50)
            {
                if ((int)(Random.Range(1, 3)) == 1)
                {
                    if (medkitAmount <= 8)
                    {
                        Instantiate(health, healthSpawns[Random.Range(0, 10)]);
                        Debug.Log("health spawned");
                        medkitAmount++;
                    }
                    else
                    {
                        Debug.Log("too many medkits spawned");
                    }
                }
                else
                {
                    if (ammoAmount <= 8)
                    {
                        Instantiate(ammo, ammoSpawns[Random.Range(0, 10)]);
                        Debug.Log("ammo spawned");
                        ammoAmount++;
                    }
                    else
                    {
                        Debug.Log("too much ammo spawned");
                    }
                }
            }
        }
        else
        {
            if ((int)(Random.Range(1, 5000)) == 50)
            {
                if ((int)(Random.Range(1, 3)) == 1)
                {
                    if (medkitAmount <= 8)
                    {
                        Instantiate(health, healthSpawns[Random.Range(0, 10)]);
                        Debug.Log("health spawned");
                        medkitAmount++;
                    }
                    else
                    {
                        Debug.Log("too many medkits spawned");
                    }
                }
                else
                {
                    if (ammoAmount <= 8)
                    {
                        Instantiate(ammo, ammoSpawns[Random.Range(0, 10)]);
                        Debug.Log("ammo spawned");
                        ammoAmount++;
                    }
                    else
                    {
                        Debug.Log("too much ammo spawned");
                    }
                }
            }

        }
    }
}
