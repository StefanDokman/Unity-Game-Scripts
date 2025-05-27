using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public AudioSource audioSource;
    public ParticleSystem Spark;
    public ParticleSystem Flash;
    public ParticleSystem Fire;
    public ParticleSystem Smoke;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.layer == 7)
        {
            audioSource.Play();
            this.GetComponent<SphereCollider>().radius = 2.5f;
            Spark.Play();
            Spark.enableEmission = true;
            Flash.Play();
            Flash.enableEmission = true;
            Fire.Play();
            Fire.enableEmission = true;
            Smoke.Play();
            Smoke.enableEmission = true;
            StartCoroutine(selfDestruct());
        }
    }
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
