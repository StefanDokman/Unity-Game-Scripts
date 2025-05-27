using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public int MaxStamina=100;
    public float CurrentStamina;
    void Start()
    {
        CurrentStamina = MaxStamina;
        staminaBar.maxValue = MaxStamina;
        staminaBar.value = CurrentStamina;
        StartCoroutine(Regen());
    }


    void Update()
    {
        staminaBar.value = CurrentStamina;
        if (Input.GetMouseButton(1) && CurrentStamina>=0)
        {
            CurrentStamina -= 10f*Time.deltaTime;
            Time.timeScale = 0.5f;
            StopAllCoroutines();

        }

        if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1f;
            StartCoroutine(Regen());
            
        }
    }

    IEnumerator Regen()
    {
        
        yield return new WaitForSeconds(2);
        while (CurrentStamina < MaxStamina)
        {
            CurrentStamina += MaxStamina / 100;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
