using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns/Revolver")]
class Revolver : Gun
{

    private void OnEnable()
    {
        damage = 50;
        currentAmmo = 6;
        totalAmmo = 32;
        firingCooldown = Time.time + 1 / 2f;
        reloadTime = 2;
        clipSize = 6;
    }

    public override void attack(WeaponController gun)
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            gun.muzzleFlash.Play();
            gun.gunAnimator.Play("Kickback");
            RaycastHit hit;
            if (Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
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
                    hit.collider.GetComponent<Rigidbody>().AddForceAtPosition((Camera.main.transform.forward + new Vector3(0, 1, 0)) * 10, hit.point, ForceMode.Impulse);
                }
            }
            firingCooldown = Time.time + 1 / 2f;
        }
        else
        {
            if (totalAmmo > 0)
                gun.StartReload(this);
        }
    }
}
