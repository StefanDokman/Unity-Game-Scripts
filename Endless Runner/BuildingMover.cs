using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMover : MonoBehaviour
{
    public GameObject[] buildings;
    public float speed;
    public int spawnAngle;
    public float spawnTime;
    void Start()
    {
        StartCoroutine(spawnBuildings());
    }

    
    void Update()
    {
        for( int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position += new Vector3(0,0,-speed * Time.deltaTime);
        }
    }

    IEnumerator spawnBuildings()
    {
        while (true)
        {
            int number = Random.Range(0, buildings.Length);
            GameObject building = Instantiate(buildings[number].gameObject);
            building.transform.SetParent(transform);
            building.transform.localPosition = Vector3.zero;
            building.transform.localRotation = Quaternion.Euler(0, spawnAngle, 0);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
