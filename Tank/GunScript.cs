using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunScript : MonoBehaviour
{
    //movement
    //public int MovementSpeed = 1;
    //shooting
    public GameObject bulletPre;
    public Transform BulletPoint;
    public bool isReloading = false;
    public int maxBullets = 7;
    public int currentBullets;
    public float ReloadTime = 1.0f;
    public Rigidbody2D rb;
    public int bulletForce=11;
    private float NextTimeToFire = 0f;
    public float FireRate = 5f;
    public float angle;
    public int rotationSpeed=1;
    //rotation
    Vector2 lookDir;
    Vector2 mousePosition;

    void Start()
    {
        currentBullets = maxBullets;
    }

    void Update()
    {
        //movement
        //var movementX = Input.GetAxis("Horizontal");
        //var movementY = Input.GetAxis("Vertical");
        //transform.position += new Vector3(movementX, movementY, 0) * Time.deltaTime * MovementSpeed;
       //slow motion
        //float time = (movementX != 0 || movementY != 0) ? 1f : .03f;
        //float lerpTime = (movementX != 0 || movementY != 0) ? 0.5f : .5f;
        //Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime); 




        //rotation
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
    private void FixedUpdate()
    {
        //rotation
        lookDir = mousePosition - rb.position;
        angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg* -1 ;

        rb.rotation = angle;
        //reload
        if (currentBullets <= 0 && isReloading == false)
            StartCoroutine(Reload());
        //shooting
        if (Input.GetMouseButton(0) && currentBullets > 0 && Time.time >= NextTimeToFire && isReloading == false)
        {
            NextTimeToFire = Time.time + 1f / FireRate;
            currentBullets = currentBullets - 1;
            Shoot();
        }

    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPre, BulletPoint.position,BulletPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(BulletPoint.up * bulletForce, ForceMode2D.Impulse);

    }
    IEnumerator Reload()
    {

        isReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        currentBullets = maxBullets;
        isReloading = false;
       
    }

}
