using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject lightObject;
    private Animator animatorLight;
    private Animator animatorPlayer;
    public GameObject obstacleSpawner;
    public GameObject buildingSpawnerLeft;
    public GameObject buildingSpawnerRight;
    public GameObject roadSpawner;
    public GameObject groundSpawner;
    void Start()
    {
        animatorLight = lightObject.GetComponent<Animator>();
        animatorPlayer = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            animatorLight.Play("coinPickupLight");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            animatorPlayer.Play("Death");
            obstacleSpawner.GetComponent<ObstacleSpawner>().enabled = false;
            buildingSpawnerLeft.GetComponent<BuildingMover>().enabled = false;
            buildingSpawnerRight.GetComponent<BuildingMover>().enabled = false;
            roadSpawner.GetComponent<BuildingMover>().enabled=false;
            groundSpawner.GetComponent<BuildingMover>().enabled=false;
        }
    }
}
