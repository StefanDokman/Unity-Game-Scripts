using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{


    //shooting
    public GameObject bulletPre;
    public Transform BulletPoint;
    private Rigidbody rb;
    public float maxForce = 11f;
    public float currentForce = 0f;
    public float Torque = 0.1f;
    public bool isShooting=false;

    private void Awake()
    {
        currentForce = 0f;
    }

    private void Update()
    {


        if (Input.GetMouseButton(0) )
        {
            if (currentForce<maxForce)
            currentForce += Torque * Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (currentForce> 3)
            Shoot();
            currentForce = 0f;
        }

    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPre, BulletPoint.position, BulletPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(BulletPoint.forward * currentForce, ForceMode.Impulse);

    }


}
