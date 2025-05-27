using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnTime = 0;
    public int spawnMax = 10;
    public Transform parrent;
    void Start()
    {
        
    }

    
    void Awake()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < spawnMax; i++)
        {
            yield return new WaitForSeconds(spawnTime);
            if(parrent==null)parrent = this.gameObject.transform;
            Instantiate(enemyToSpawn, transform.position, transform.rotation,parrent);
        }
    }
}
