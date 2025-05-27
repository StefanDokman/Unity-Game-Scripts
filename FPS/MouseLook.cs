using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity = 100f;

    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation = -mouseY;
        if (this.transform.rotation.eulerAngles.x >= 70f && this.transform.rotation.eulerAngles.x <180 && xRotation>0)
        {
            xRotation = 0;
        }
        else if (this.transform.rotation.eulerAngles.x <= 280f && this.transform.rotation.eulerAngles.x >180f && xRotation<0)
        {
            xRotation = 0;
        }

        this.transform.Rotate(xRotation,0,0);
        playerBody.Rotate(Vector3.up * mouseX);


    }

}
