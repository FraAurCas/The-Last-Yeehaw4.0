using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public IWeapon _weapon;
    public List<IWeapon> weapons;

    public Text ammoCounter;
    public GameObject gun;
    public GameObject knife;
    public GameObject cam;
    public ParticleSystem muzzleFlash;
    public ParticleSystem revolverProjectile;
    public ParticleSystem shotgunProjectile;
    public ParticleSystem repeaterProjectile;
    public Animator gunAnimator;
    public Animator knifeAnimator;


    public bool reloading = false;
    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<IWeapon>();
        weapons.Add(ScriptableObject.CreateInstance<Revolver>());
        weapons.Add(ScriptableObject.CreateInstance<Shotgun>());
        weapons.Add(ScriptableObject.CreateInstance<Repeater>());
        weapons.Add(ScriptableObject.CreateInstance<Knife>());
        _weapon = weapons[0];
    }

    // Update is called once per frame
    void Update()
    {
        //If the player left clicks the gun shootes
        
        if (!reloading && Input.GetKeyDown(KeyCode.Q))
        {
            _weapon = weapons[(weapons.IndexOf(_weapon) + 1) % weapons.Count];
            if (_weapon is Gun)
            {
                gun.SetActive(true);
                knife.SetActive(false);
                ammoCounter.text = string.Format("{0}/{1}", ((Gun)_weapon).currentAmmo, ((Gun)_weapon).totalAmmo);
            }
            else
            {
                gun.SetActive(false);
                knife.SetActive(true);
                ammoCounter.text = string.Format("{0}", "Knife");
            }
        }

        if (reloading)
        {
            ammoCounter.text = string.Format("{0}", "Reloading");
        }

        else {
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && Time.time > _weapon.AttackCooldown)
            {
                _weapon.attack(this);
                if(_weapon is Gun)
                    ammoCounter.text = string.Format("{0}/{1}", ((Gun)_weapon).currentAmmo, ((Gun)_weapon).totalAmmo);
            }

            if (_weapon is Gun && Input.GetKeyDown(KeyCode.R))
            {
                StartReload((Gun)_weapon);
            }

            if (Input.GetMouseButtonDown(1)){
                GetComponent<Camera>().fieldOfView = 30;     
            }
            else if (Input.GetMouseButtonUp(1)){
                GetComponent<Camera>().fieldOfView= 60;
            }
        }
    }

    public void StartReload(Gun gun)
    {
        reloading = true;
        StartCoroutine(reload(gun));
    }

   /* void checkShoot()
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

    }*/

    IEnumerator reload(Gun gun)
    {
        gunAnimator.SetFloat("ReloadSpeed", 2 / gun.reloadTime);
        if(gun.currentAmmo != gun.clipSize && gun.totalAmmo >= gun.clipSize)
        {
            gunAnimator.Play("Reloading");
            //Waits for two seconds, in order to simulate reload times
            yield return new WaitForSeconds(gun.reloadTime);

            int ammoDif = gun.clipSize - gun.currentAmmo;
            gun.currentAmmo += ammoDif;
            gun.totalAmmo -= ammoDif;
            reloading = false;
        }
        else if (gun.totalAmmo > 0 && gun.totalAmmo <= gun.clipSize)
        {
            gunAnimator.Play("Reloading");
            //Waits for two seconds, in order to simulate reload times
            yield return new WaitForSeconds(gun.reloadTime);

            
            gun.currentAmmo = gun.totalAmmo;
            gun.totalAmmo = 0;
            reloading = false;
        }
        ammoCounter.text = string.Format("{0}/{1}", gun.currentAmmo, gun.totalAmmo);

    }
}