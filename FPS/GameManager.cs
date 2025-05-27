using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Boolean isPaused = false;
    public Boolean isPossible = true;
    public GameObject PausePanel;
    public GameObject WinPanel;
    public GameObject Obstacles;
    public GameObject Player;
    public GameObject Enviorment;
    public GameObject camera;
    public GameObject hand;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused && isPossible) 
            {
                UnPause();
            }
            else if (!isPaused && isPossible)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Player.GetComponent<PlayerMovement>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
        hand.SetActive(false);
        camera.GetComponent<AudioSource>().Pause();
        Enviorment.SetActive(false);
        Obstacles.SetActive(false);
        PausePanel.SetActive(true);

        isPaused = true;
    }

    public void UnPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player.GetComponent<PlayerMovement>().enabled = true;
        camera.GetComponent<MouseLook>().enabled = true;
        hand.SetActive(true);
        camera.GetComponent<AudioSource>().Play();
        Enviorment.SetActive(true);
        Obstacles.SetActive(true);
        PausePanel.SetActive(false);

        isPaused = false;
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Player.GetComponent<PlayerMovement>().enabled = false;
        camera.GetComponent<MouseLook>().enabled = false;
        hand.SetActive(false);
        camera.GetComponent<AudioSource>().Pause();
        Enviorment.SetActive(false);
        Obstacles.SetActive(false);
        WinPanel.SetActive(true);
        isPossible = false;
    }



    public void Exit()
    {
        Application.Quit();
    }
}
