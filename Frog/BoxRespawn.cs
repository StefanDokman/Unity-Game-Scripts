using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    public GameObject Box;
    public Vector3 respawnPoint;

    void Start()
    {
        Box = GameObject.Find("Box");
        respawnPoint = new Vector3(Box.transform.position.x, Box.transform.position.y, Box.transform.position.z);
    }

    
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "DeathZone")
        {
            transform.position = respawnPoint;
        }


    }

}
