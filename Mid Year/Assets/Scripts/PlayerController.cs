﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour 


{
    public float speed = 4.0f;
    public float gravity = -9.8f;

    public Image healthBar;
    public Image staminaBar;

    public Canvas hud;
    public Canvas gameOverScreen;
    public Canvas pauseScreen;

    private float stamina = 100f;
    private float staminaDecrease = .25f;
    private float walkingSpeed = 4.0f;

    private int health = 100;
    private float healthCooldown;
    private bool recoveringStamina = false;

    private float healAmount = 50.0f;
    private float damageAmount = 10.0f;

    public GameObject world;
    public GameObject gun;
    public Camera mainCamera;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameOverScreen.GetComponent<Canvas>().enabled = false;
    }

void Update()
    {
        checkSprint();
        movePlayer();
        checkPauseMenu();
    }

    void movePlayer()
    {
    
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaY);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    public bool playerIsAlive()
    {
        return health>0;
    }

    public double getHealth()
    {
        return health;
    }

    public double getStamina()
    {
        return stamina;
    }

    public bool isRecovering()
    {
        return recoveringStamina;
    }

    void checkSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && !recoveringStamina)
        {
            speed = walkingSpeed * 2.0f;
            stamina -= staminaDecrease;
            staminaBar.fillAmount -= staminaDecrease / 100;

        }

        else if (stamina < 100)
        {
            stamina += staminaDecrease / 2;
            staminaBar.fillAmount += staminaDecrease / 200;
            speed = walkingSpeed;

            recoveringStamina = true;

            if (stamina >= 50)
            {
                recoveringStamina = false;
            }
        }
    }

    private void checkPauseMenu()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseScreen.enabled = true;
            pauseScreen.GetComponent<PauseMode>().enabled = true;
        }
    }

    private void OnDisable()
    {
        //Shows the game over screen
        hud.GetComponent<Canvas>().enabled = false;

        if(health <= 0)
            gameOverScreen.GetComponent<Canvas>().enabled = true;

        //Disables all scripts
        GetComponent<CameraController>().enabled = false;
        gun.GetComponent<WeaponController>().enabled = false;
        mainCamera.GetComponent<CameraController>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        //Shows the hud again;
        hud.GetComponent<Canvas>().enabled = true;

        //Enables all scripts
        GetComponent<CameraController>().enabled = true;
        gun.GetComponent<WeaponController>().enabled = true;
        mainCamera.GetComponent<CameraController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && Time.time > healthCooldown)
        {
            health -= (int)damageAmount;
            healthBar.fillAmount -= damageAmount / 100;
            if (health <= 0)
            {

                GetComponent<PlayerController>().enabled = false;
            }

            healthCooldown = Time.time + 1 / 2f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    
        if(other.gameObject.tag.Equals("Medkit") && health < 100)
        {
            if (health > 100 - healAmount)
            {
                health = 100;
                healthBar.fillAmount = 1;
            }

            else
            {
                health += (int)healAmount;
                healthBar.fillAmount += healAmount/100;
            }
            Destroy(other.gameObject);
            world.GetComponent<WorldController>().TakenMed();
        }

        if (other.gameObject.tag.Equals("Ammo"))
        {
            foreach(IWeapon weapon in gun.GetComponent<WeaponController>().weapons)
            {
                if(weapon is Gun)
                    ((Gun)weapon).totalAmmo += ((Gun)weapon).clipSize;
            }
            Destroy(other.gameObject);
            world.GetComponent<WorldController>().TakenAmmo();
        }
    }
}