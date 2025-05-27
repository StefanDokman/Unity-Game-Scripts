using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        
    }


    void Update()
    {
        transform.position = Player.transform.position;
    }
}
