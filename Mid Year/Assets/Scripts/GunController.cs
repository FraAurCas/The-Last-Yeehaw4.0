using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    private float gunDamage = 50;
    private float headshotDamage = 100;

    public int currentAmmo = 6;
    public int totalAmmo = 32;
    public Text ammoCounter;

    public ParticleSystem muzzleFlash;
    public Animator gunAnimator;

    private float firingCooldown;
    private float reloadTime = 2;

    public bool reloading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (totalAmmo < 0)
            totalAmmo = 0;
        //If the player left clicks the gun shootes
        if (reloading)
        {
            ammoCounter.text = string.Format("{0}", "Reloading");
        }

        else {
            if (Input.GetMouseButton(0) && Time.time > firingCooldown)
            {
                checkShoot();
            }

            if (Input.GetKey(KeyCode.R))
            {
                ammoCounter.text = string.Format("{0}", "Reloading");
                StartCoroutine(reload());
            }


            ammoCounter.text = string.Format("{0}/{1}", currentAmmo, totalAmmo);
        }
    }

    void checkShoot()
    {
        if(currentAmmo > 0)
        {
            currentAmmo--;
            muzzleFlash.Play();
            gunAnimator.Play("Kickback");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Enemy")
                {
                    //Causes the zombie to take damage, and destroys it if it's dead
                    if (hit.collider.GetComponent<ZombieController>().takeDamage(gunDamage))
                    {
                        Destroy(hit.collider.gameObject);
                    }

                }

                if(hit.collider.tag == "Target")
                {
                    hit.collider.GetComponent<Rigidbody>().AddForceAtPosition((Camera.main.transform.forward + new Vector3(0, 1, 0)) * 10, hit.point, ForceMode.Impulse);
                }
            }

            firingCooldown = Time.time + 1 / 2f;
        }

        else
        {
            StartCoroutine(reload());
        }

    }

    IEnumerator reload()
    {
        if(!(currentAmmo == 6 && totalAmmo>=6))
        {
            reloading = true;
            gunAnimator.Play("Reloading");
            //Waits for two seconds, in order to simulate reload times
            yield return new WaitForSeconds(reloadTime);

            int ammoDif = 6 - currentAmmo;
            currentAmmo += ammoDif;
            totalAmmo -= ammoDif;
            reloading = false;
        }
        else if (totalAmmo >0 && totalAmmo <= 6)
        {
            reloading = true;
            gunAnimator.Play("Reloading");
            //Waits for two seconds, in order to simulate reload times
            yield return new WaitForSeconds(reloadTime);

            
            currentAmmo = totalAmmo;
            totalAmmo = 0;
            reloading = false;
        }

    }
}
