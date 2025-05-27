using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetection : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerMovement movement; 
    public GameObject gameManager;
    public AudioClip win;
    public AudioClip death;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Death")
        {
            GetComponent<AudioSource>().clip = death;
            GetComponent<AudioSource>().Play();
            gameManager.GetComponent<GameManager>().Pause();
            gameManager.GetComponent<GameManager>().isPossible = false;
            
        }
        if(other.tag == "Door")
        {
            GetComponent<AudioSource>().clip = win;
            GetComponent<AudioSource>().Play();
            gameManager.GetComponent<GameManager>().Win();
            gameManager.GetComponent<GameManager>().isPossible = false;
        }
    }

}
