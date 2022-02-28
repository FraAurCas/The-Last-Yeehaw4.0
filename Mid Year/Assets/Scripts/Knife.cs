using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Knife : Melee 
{
    private void OnEnable()
    {
        damage = 45;
        attackCooldown = Time.time + 0.2f;
    }
    public override void attack(WeaponController knife)
    {
        knife.knifeAnimator.Play("Stab");
        RaycastHit hit;
        if (Physics.Raycast(knife.transform.position, knife.transform.TransformDirection(Vector3.forward), out hit, 3f))
        {
            if (hit.collider.tag == "Enemy")
            {
                //Causes the zombie to take damage, and destroys it if it's dead
                if (hit.collider.GetComponent<ZombieController>().takeDamage(damage))
                {
                    Destroy(hit.collider.gameObject);
                }

            }

            if (hit.collider.tag == "Target")
            {
                hit.collider.GetComponent<Rigidbody>().AddForceAtPosition((Camera.main.transform.forward + new Vector3(0, 1, 0)) * 6, hit.point, ForceMode.Impulse);
            }
        }
        attackCooldown = Time.time + 0.2f;
    }

}
