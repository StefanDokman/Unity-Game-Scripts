using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Scene sc;
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }
}
