using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR;


public class GunScript : MonoBehaviour
{

    //shooting
    public GameObject bulletPre;
    public GameObject flashSprite;
    public Transform BulletPoint;
    public bool isReloading = false;
    public int maxBullets = 7;
    public int currentBullets;
    public float ReloadTime = 1.0f;
    public int bulletForce=11;
    private float NextTimeToFire = 0f;
    public float FireRate = 5f;
    private GameObject ammo;
    public Animator animator;
    public GameObject reloadText;
    public bool isUnlocked = true;
    private GameObject camera;
    public float recoilAmountX = 5;
    public float recoilAmountY = 5;
    public AudioClip audioClip;
    public AudioClip reloadSound;
    private void OnEnable()
  
    {
        isReloading = false;
        ammo = GameObject.Find("Ammo");
        //reloadText = GameObject.Find("ReloadText");
        camera = GameObject.Find("Main Camera");
        ammo.GetComponent<TextMeshProUGUI>().SetText(currentBullets.ToString() + "/" + maxBullets.ToString());
        
        
    }
    void Start()
    {   
        currentBullets = maxBullets;
        // reloadText = GameObject.Find("ReloadText");
        ammo.GetComponent<TextMeshProUGUI>().SetText(currentBullets.ToString() + "/" + maxBullets.ToString());
        ammo = GameObject.Find("Ammo");
        camera = GameObject.Find("Main Camera");
    }

    private void FixedUpdate()
    {

        //reload
        if (Input.GetKeyDown("r"))
            StartCoroutine(Reload());
        //shooting
        if (Input.GetMouseButton(0) && currentBullets > 0 && Time.time >= NextTimeToFire && isReloading == false)
        {
            NextTimeToFire = Time.time + 1f / FireRate;
            currentBullets = currentBullets - 1;
            Shoot();
        }
        if (currentBullets == 0)
            reloadText.SetActive(true);
        else reloadText.SetActive(false);

    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPre, BulletPoint.position,BulletPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(BulletPoint.forward * bulletForce, ForceMode.Impulse);
        StartCoroutine(Flash());
        StartCoroutine(PlayAudio());
        camera.transform.Rotate(new Vector3(camera.transform.rotation.x + ((Random.value - .5f) / 2) * recoilAmountX, 0, 0));
        ammo.GetComponent<TextMeshProUGUI>().SetText(currentBullets.ToString() + "/" + maxBullets.ToString());

    }

    IEnumerator PlayAudio()
    { 
        AudioSource audioS;
        audioS = this.gameObject.AddComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = audioClip;
        audioS.Play();
        yield return new WaitForSeconds(1);
        Destroy(audioS);
    }

    IEnumerator PlayReload()
    {
        AudioSource audioS;
        audioS = this.gameObject.AddComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = reloadSound;
        audioS.Play();
        yield return new WaitForSeconds(2);
        Destroy(audioS);
    }


    IEnumerator Reload()
    {

        isReloading = true;
        animator.SetBool("isReloading", true);
        StartCoroutine(PlayReload());
        yield return new WaitForSeconds(ReloadTime);
        animator.SetBool("isReloading", false);
        currentBullets = maxBullets;
        ammo.GetComponent<TextMeshProUGUI>().SetText(currentBullets.ToString() + "/" + maxBullets.ToString());
        isReloading = false;
       
    }
    IEnumerator Flash()
    {
        flashSprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flashSprite.SetActive(false);
    }

}
