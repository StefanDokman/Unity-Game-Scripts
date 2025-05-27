using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreAmmount;
    private Text scoreText;
    
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreAmmount = 0;

    }
    private void Update()
    {
    if(scoreText!=null)
        scoreText.text = scoreAmmount.ToString();

    }
}
