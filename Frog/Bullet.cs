using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = -1;
    public GameObject bullet;
    private Rigidbody2D _rigidbody;
    public float BulletDeathTime=3.0f;

    void Start()
    {
        this.bullet = gameObject;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletDeathTime);
            Destroy(bullet.gameObject);


        }
    }


    void Update()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }
}


