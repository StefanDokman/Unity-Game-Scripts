using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectorFox : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject myGameObject;
    public GameObject DeathDetector;
    public GameObject Player;
    private Rigidbody2D _rigidbody;
    public float Launch = 2;


    void Start()
    {
        this.myGameObject = gameObject;
        Enemy = gameObject.transform.parent.gameObject;
        Player = GameObject.Find("Player");
        _rigidbody = Player.GetComponent<Rigidbody2D>();
        DeathDetector = GameObject.Find("DeathDetector2");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemy.GetComponent<BoxCollider2D>().enabled = false;
            myGameObject.GetComponent<BoxCollider2D>().enabled = false;
            _rigidbody.AddForce(new Vector2(0, Launch), ForceMode2D.Impulse);
        }



    }


}
