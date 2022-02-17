using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverProjectile : MonoBehavior
{
    public ParticleSystem particleSystem;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    // Start is called before the first frame update
    void Start()
    {
     	particleSystem = GetComponent<ParticleSystem>();   
    }

    // Update is called once per frame
    private void Update()
    {
	if(Input.GetKeyDown(KeyCode.Mouse0))
	{
	    particleSystem.Play();
	}
        
    }

    private void OnParticleCollision(GameObject other)
    {
	int events = particleSystem.GetCollisionEvents(other, colEvents);
	Debug.Log("Hit");
	
	for (int i = 0; i < events; i++)
	{

	}
	
	
    }
}
