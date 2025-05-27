using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletDeathTime=3.0f;


    void Start()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
            yield return new WaitForSeconds(BulletDeathTime);
            Destroy(this.gameObject);



    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }


    }

}


