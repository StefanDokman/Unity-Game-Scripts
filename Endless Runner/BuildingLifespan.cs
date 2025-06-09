using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLifespan : MonoBehaviour
{
    public int destroyDistance;
    public bool isSmallerThen = true;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.localPosition.z < destroyDistance && isSmallerThen) Destroy(gameObject);
        if (transform.localPosition.z > destroyDistance && !isSmallerThen) Destroy(gameObject);
    }
}
