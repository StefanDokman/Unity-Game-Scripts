using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    GameObject Player;
    public GameObject enemy;
    public float xRandomMin = 20;
    public float xRandomMax = 25;
    public float yRandomMin = 20;
    public float yRandomMax = 25;
    public float SpawnTime = 2;
    public float MovementSpeed = 3f;
    bool ifWait = true;

    void Start()
    {
        Player = GameObject.Find("Player");
        
    }


    void Update()
    {
        transform.position = Player.transform.position;
        if (ifWait)
        StartCoroutine(Spawn());
        
        
    }

    IEnumerator Spawn()
    {
            
            ifWait = false;
            yield return new WaitForSeconds(SpawnTime);
            Instantiate(enemy, transform.position + new Vector3(Random.Range(xRandomMin * (Random.Range(0, 2) * 2 - 1), xRandomMax * (Random.Range(0, 2) * 2 - 1)), Random.Range(yRandomMin * (Random.Range(0, 2) * 2 - 1), yRandomMax* (Random.Range(0, 2) * 2 - 1)),0), transform.rotation);
            ifWait = true;
        

    }
}
