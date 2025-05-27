using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject bullet;
    public Transform BulletPoint;
    public float Reload = 1.0f;


    void Start()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(Reload);
            Instantiate(bullet, BulletPoint.position, BulletPoint.rotation);
        }
    }

    void Update()
    {

    }
}
