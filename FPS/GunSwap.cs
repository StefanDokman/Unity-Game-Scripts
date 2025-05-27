using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunSwap : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1)) { weapon1.SetActive(true); weapon2.SetActive(false); }
        if (Input.GetKeyUp(KeyCode.Alpha2)) { weapon2.SetActive(true); weapon1.SetActive(false); }

    }
}
