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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" || other.gameObject.layer == 7)
        {
            Destroy(this.gameObject);
        }
    }

}


