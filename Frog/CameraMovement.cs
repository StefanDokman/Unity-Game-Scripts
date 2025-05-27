using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }


   private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y , Player.transform.position.z - 10);
    }

}
