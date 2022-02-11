using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    GameObject player;
    NavMeshAgent nav;
    public GameObject zom;
    private float zombieHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        waitbit();
        player = GameObject.FindGameObjectWithTag("Player");
        NavMeshHit closestHit;
        if(NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        {
            player.transform.position = closestHit.position;
            Debug.Log("Zombie is working");
        }
        else
        {
            Debug.Log("Couldn't spawn zombie");
        }
        nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
        zombieHealth = 100;
    }

    private IEnumerator waitbit()
    {
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {   if (isActiveAndEnabled && GameObject.Find("Player").GetComponent<PlayerController>().playerIsAlive()) {
            if (zombieHealth > 0 && player.transform.position != null)
            {
                nav.SetDestination(player.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
    }

    //Causes the zombie to take damage, and checks to see if it's dead
    public bool takeDamage(float damageAmount)
    {
        zombieHealth -= damageAmount;
        if(zombieHealth <= 0)
            return true;

        return false;
    }
}
