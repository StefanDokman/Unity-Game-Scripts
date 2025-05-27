using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMove : MonoBehaviour
{
    public float MaxMoveDown = 10;
    public float speed = -1f;
    public float moved = 0f;
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        if (moved > MaxMoveDown) { 
            speed *= -1f;
            moved = 0f;
        }
        this.transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        moved +=Mathf.Abs(speed * Time.deltaTime);
    }
}
