using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollider : MonoBehaviour
{
    public Slider health;
    public GameObject parrent;
    int damage = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.value <= 48)
          Destroy(parrent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="bullet")
            health.value -= damage;
    }
}
