using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSensor : MonoBehaviour
{
        private bool isActivated=false;
    public GameObject sensorProps;
    public GameObject sensorEnemies;
    public AudioClip audioClip;
    private bool isEnded = false;
    public bool isLast = false;
    public GameObject gameManager;
    void Start()
    {
        
    }


    void Update()
    {
        if(sensorEnemies.transform.childCount==0 && isEnded == false) {
            sensorProps.SetActive(false);
            StartCoroutine(PlayAudio());
            isEnded = true;
            if (isLast)
            {
                gameManager.GetComponent<GameManager>().Win();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && isActivated==false) {
            isActivated=true;
            StartCoroutine(PlayAudio());
            sensorProps.SetActive(true);
            sensorEnemies.SetActive(true);
        }
    }

    IEnumerator PlayAudio()
    {
        AudioSource audioS;
        audioS = this.gameObject.AddComponent<AudioSource>();
        audioS.loop = false;
        audioS.clip = audioClip;
        audioS.Play();
        yield return new WaitForSeconds(1.5f);
        audioS.Stop();
        Destroy(audioS);
    }
}
