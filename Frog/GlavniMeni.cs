using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlavniMeni : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
