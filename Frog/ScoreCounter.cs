using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public GameObject s;
    public int scoreAmmount;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (s) 
       scoreAmmount= s.GetComponent<Score>().scoreAmmount;
    }
}
