using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public int MovementSpeed = 1;
    public Rigidbody2D rb;
    float tiltAngle = 180.0f;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    { 
        var movementX = Input.GetAxis("Horizontal") * tiltAngle;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0,0, movementX * Time.deltaTime * -1));
        if (Input.GetKey("w")) {
            transform.position += transform.up * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.up * Time.deltaTime * MovementSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            this.GetComponent<HealthSystem>().CurrentHealth--;
            Destroy(collision.gameObject);
        }
    }
}

