using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    private float zombieHealth;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        zombieHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {   if (isActiveAndEnabled && GameObject.Find("Player").GetComponent<PlayerController>().playerIsAlive()) {
            if (zombieHealth > 0 && player.position != null)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
    }

    public bool takeDamage(float damageAmount)
    {
        zombieHealth -= damageAmount;
        if(zombieHealth <= 0)
        {
            Debug.Log("I'm Dead!");
            return true;
        }

        return false;
    }
}
