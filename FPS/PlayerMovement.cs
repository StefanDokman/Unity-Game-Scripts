using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
   public CharacterController controller;

    public float speed = 12f;
    public float gravity = -10f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;

    public Slider HealthBar;

    public AudioClip audioClip;
    public GameManager gameManager;


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = MathF.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="enemy") {
            StartCoroutine(PlayAudio());
            HealthBar.value -= 5;
            if(HealthBar.value <= 60) {
                gameManager.GetComponent<GameManager>().Pause();
                gameManager.GetComponent<GameManager>().isPossible = false;
            }
        }
    }

    IEnumerator PlayAudio()
    {
        AudioSource audioS;
        audioS = this.gameObject.AddComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = audioClip;
        audioS.Play();
        yield return new WaitForSeconds(1);
        Destroy(audioS);
    }

}

