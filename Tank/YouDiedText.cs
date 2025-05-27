using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDiedText : MonoBehaviour
{

    void Start()
    {
        this.gameObject.SetActive(false);
    }


    void Update()
    {

    }

    public void Dead(){
        this.gameObject.SetActive(true);
    }
}
