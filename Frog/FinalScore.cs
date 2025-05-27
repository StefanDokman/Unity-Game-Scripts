using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    GameObject scoreCount;
    public int scoreAmmount;
    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreAmmount = 0;
        scoreCount = GameObject.Find("ScoreCounter");
        if(scoreCount)
        scoreAmmount= scoreCount.GetComponent<ScoreCounter>().scoreAmmount;
        if (scoreText != null)
            scoreText.text = scoreAmmount.ToString() + "/63";
    }

}
