using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMode : MonoBehaviour
{
    public PlayerController playerController;
    public Canvas pauseScreen;
    public float cooldown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        playerController.enabled = false;
        pauseScreen.enabled = true;
    }

    public void resumeGame()
    {
        playerController.enabled = true;
        pauseScreen.enabled = false;
        GetComponent<PauseMode>().enabled = false;
    }

}
