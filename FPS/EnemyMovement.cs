using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 1f;
    public int maxHealth = 10;
    public int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet") 
        {
            currentHealth -= 4;
            if(currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if(collision.gameObject.tag == "granade")
        {
            currentHealth -= 10;
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
