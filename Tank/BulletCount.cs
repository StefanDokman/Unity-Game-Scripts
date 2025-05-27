using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    private Text BulletNumber;
    private GameObject Gun;
    private GunScript gunScript;
    int MaxBulets;
    int CurentBullets;


    void Start()
    {
        BulletNumber = GetComponent<Text>();
        Gun = GameObject.Find("Gun");
        
        
    }
    private void Update()
    {
        MaxBulets = Gun.GetComponent<GunScript>().maxBullets;
        CurentBullets = Gun.GetComponent<GunScript>().currentBullets;
        BulletNumber.text = CurentBullets.ToString() +"/"+ MaxBulets.ToString(); 
            
    }
}
