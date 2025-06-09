using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenMover : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }
}
