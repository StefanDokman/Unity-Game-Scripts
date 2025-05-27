using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D _rigidbody;
    public Vector3 respawnPoint;
    public GameObject Player;
    public GameObject Score;



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        Score = GameObject.Find("Score");
        respawnPoint = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
    }


    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        if (!Mathf.Approximately(0, movement))
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            Player.transform.SetParent(null);
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "DeathZone")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.tag == "Collect")
        {
            Destroy(other.gameObject);
            Score.GetComponent<Score>().scoreAmmount = Score.GetComponent<Score>().scoreAmmount + 1;
        }

        if (other.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.tag == "Secret") 
        {
            SpriteRenderer sp = other.gameObject.GetComponent<SpriteRenderer>();
            sp.sortingOrder = -2;
        }
    }




}