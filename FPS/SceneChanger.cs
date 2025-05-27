using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Scene sc;
    public void RedArena()
    {
        SceneManager.LoadScene("RedArena",LoadSceneMode.Single);
    }
    public void MainMenu()
    {
        Debug.Log("1");
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }
    public void SpookyTown()
    {
        SceneManager.LoadScene("SpookyTown", LoadSceneMode.Single);
    }
    public void Dungeon()
    {
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
