using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class HealthSystem : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;
    public GameObject YouDied;
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        int i;
        for (i = 0; i < hearts.Length; i++)
        {
            if (i < CurrentHealth)
                hearts[i].sprite = fullHearts;
            else hearts[i].sprite = emptyHearts;

            if (i < MaxHealth)
                hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
        if (CurrentHealth == 0) {
            GameObject.Find("Canvas").GetComponent<PauseMenu>().Pause();
            YouDied.GetComponent<YouDiedText>().Dead();
        }
    }

}
