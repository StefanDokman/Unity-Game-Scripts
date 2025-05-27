using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float MovementSpeed = 3f;
    GameObject Player;
    GameObject Manager;
    EnemySpawnManager script;
    public Rigidbody2D rb;
    Vector2 lookDir;
    Vector2 PlayerPosition;
    public float angle;
    void Start()
    {
        Player = GameObject.Find("Player");
        Manager = GameObject.Find("EnemySpawnManager");
        script  = Manager.GetComponent<EnemySpawnManager>();
    }


    void Update()
    {
        MovementSpeed = script.MovementSpeed;
        //ROTATION
        PlayerPosition = Player.transform.position;
        lookDir = PlayerPosition - rb.position;
        angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg * -1;
        rb.rotation = angle;
        //MOVEMENT
        transform.position += transform.up * Time.deltaTime * MovementSpeed;
    }



}