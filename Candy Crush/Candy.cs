using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Candy : MonoBehaviour
{
    public int value;
    public int gridPositionX;
    public int gridPositionY;
    public Color colorNormal;
    private Color colorSelected = Color.green;

    void Awake()
    {
        colorNormal = this.GetComponent<Renderer>().material.color;
    }


    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(gridPositionX + ", " + gridPositionY);
            if (transform.parent.gameObject.GetComponent<CandyCrush>().blockForChange1 == null)
            {
                transform.parent.gameObject.GetComponent<CandyCrush>().blockForChange1 = this.gameObject;
                StartSwaper();

            }
            else if (transform.parent.gameObject.GetComponent<CandyCrush>().blockForChange2 == null)
            {
                transform.parent.gameObject.GetComponent<CandyCrush>().blockForChange2 = this.gameObject;
                StartSwaper();

            }
        }
    }

    private void Update()
    {
        if(value == -1)
        {
            this.gameObject.SetActive(false);
        }
    }
    public IEnumerator colorSwaper(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float lerpFactor = Mathf.Clamp01(t / duration);
            Color currentColor = Color.Lerp(colorNormal, colorSelected, lerpFactor);
            this.GetComponent<Renderer>().material.color = currentColor;
            yield return null;
        }

    }

    public IEnumerator colorReturn(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float lerpFactor = Mathf.Clamp01(t / duration);
            Color currentColor = Color.Lerp(colorSelected, colorNormal, lerpFactor);
            this.GetComponent<Renderer>().material.color = currentColor;
            yield return null;
        }

    }

    public void StartReturn()
    {
        StopAllCoroutines();
        StartCoroutine(colorReturn(1));
        
    }
    public void StartSwaper()
    {
        StopAllCoroutines();
        StartCoroutine(colorSwaper(1));
    }
}


