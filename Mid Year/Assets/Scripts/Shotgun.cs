using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns/Shotgun")]
class Shotgun : Gun
{

    private void OnEnable()
    {
        damage = 100;
        currentAmmo = 2;
        totalAmmo = 40;
        firingCooldown = Time.time + 1f;
        reloadTime = 4;
        clipSize = 2;
    }

    public override void attack(WeaponController gun)
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            gun.muzzleFlash.Play();
            gun.shotgunProjectile.Play();
            gun.gunAnimator.Play("Kickback");
            // RaycastHit hit;
            // float x = -0.45f;
            // while (x <= 0.45)
            // {
            //     Debug.DrawRay(gun.transform.position, gun.transform.TransformDirection(Vector3.forward + new Vector3(x, 0, 0)) * 10, Color.red, 5f, false);
            //     if (Physics.Raycast(gun.transform.position, gun.transform.TransformDirection(Vector3.forward + new Vector3(x, 0, 0)), out hit, 15f))
            //     {
            //         if (hit.collider.tag == "Enemy")
            //         {
            //             //Causes the zombie to take damage, and destroys it if it's dead
            //             if (hit.collider.GetComponent<ZombieController>().takeDamage(damage))
            //             {
            //                 Destroy(hit.collider.gameObject);
            //             }

            //         }

            //         if (hit.collider.tag == "Target")
            //         {
            //             hit.collider.GetComponent<Rigidbody>().AddForceAtPosition((Camera.main.transform.forward + new Vector3(0, 1, 0)) * 10, hit.point, ForceMode.Impulse);
            //         }
            //     }
            //     x += 0.01f;
            // }
            
            firingCooldown = Time.time + 1f;
        }
        else
        {
            if(totalAmmo > 0)
                gun.StartReload(this);
        }
    }
}
