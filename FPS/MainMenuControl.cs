using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LevelSelection;

    public void toMenu(){
        MainMenu.SetActive(true);
        LevelSelection.SetActive(false);
    }
    public void toSelecetion()
    {
        MainMenu.SetActive(false);
        LevelSelection.SetActive(true);
    }

}
