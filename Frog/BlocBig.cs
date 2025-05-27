using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocBig : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "DeathZone")
        {
            Destroy(this.gameObject);
        }


    }
}
