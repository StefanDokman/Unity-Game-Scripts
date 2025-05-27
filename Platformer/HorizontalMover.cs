using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    private Vector3 startPosition;
    public float MaxMove = 10;
    public float speed = -1f;
    public float moved = 0f;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (moved > MaxMove)
            {
                transform.position = startPosition;
                moved = 0f;
            }
            this.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            moved += Mathf.Abs(speed * Time.deltaTime);
        }
    }
}
