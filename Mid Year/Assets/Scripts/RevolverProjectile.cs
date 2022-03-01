using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverProjectile : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject impact;
    public int damage;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    // Start is called before the first frame update
    void Start()
    {
     	particleSystem = GetComponent<ParticleSystem>();   
    }

    // Update is called once per frame
    private void Update()
    {
	// if(Input.GetKeyDown(KeyCode.Mouse0))
	// {
	//     particleSystem.Play();
	// }
        
    }

    private void OnParticleCollision(GameObject other)
    {
	int events = particleSystem.GetCollisionEvents(other, colEvents);
	
	for (int i = 0; i < events; i++)
	{
        Instantiate(impact, colEvents[i].intersection, Quaternion.LookRotation(colEvents[i].normal));
        if(other.tag == "Target")
            other.GetComponent<Rigidbody>().AddForceAtPosition((Camera.main.transform.forward + new Vector3(0, 1, 0)) * 10, colEvents[i].intersection, ForceMode.Impulse);
	}
	
    if(other.TryGetComponent(out ZombieController zombie)){
        if(zombie.takeDamage(damage)){
            Destroy(other);
        }
    }

    }
}
