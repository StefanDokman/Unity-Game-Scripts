using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    public GameObject Enemy;

    void Start()
    {
        this.Enemy = gameObject;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Limiter")
        {
            other.gameObject.transform.parent = Enemy.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Limiter" || other.tag != "PlayerHead")
        {
            other.transform.SetParent(null);
        }
    }
}