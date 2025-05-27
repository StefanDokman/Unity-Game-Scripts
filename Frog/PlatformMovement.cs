using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
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

        Enemy.transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Limiter")
        {
            movement = movement * (-1);
            if (movement == 1)
                transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            if (movement == -1)
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }

    }
}
