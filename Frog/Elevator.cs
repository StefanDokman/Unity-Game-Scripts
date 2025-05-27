using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float movement = 2;
    public float MovementSpeed = 1;
    public GameObject Enemy;

    void Start()
    {
        this.Enemy = gameObject;

    }


    void Update()
    {

        Enemy.transform.position += new Vector3(0, movement, 0) * Time.deltaTime * MovementSpeed;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Limiter")
        {
            movement = movement * (-1);

        }

    }
}
