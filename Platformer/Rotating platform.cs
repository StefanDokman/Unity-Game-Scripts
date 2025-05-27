using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatingplatform : MonoBehaviour
{

    public float speed = 10f;
    void Start()
    {

    }

    void Update()
    {

        this.transform.Rotate(new Vector3(0, speed, 0)*Time.deltaTime,Space.Self);
    }
}
