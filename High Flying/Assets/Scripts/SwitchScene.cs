using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The methods in this class are called when ever a button that causes a tranition between 
//scenes from the menu system is pressed. 
//These buttons are the "Back to Menu", "Store", "Start", Settings Cog and the level selection bottons     
public class SwitchScene : MonoBehaviour {

    [SerializeField]
    private string iceLevelName = "Ice Level";
    [SerializeField]
    private string cityLevelName = "City Level";
    [SerializeField]
    private string mainMenuLevelName = "MainPlay";
    [SerializeField]
    private string levelSelectMenuName = "LevelSelect";
    [SerializeField]
    private string settingsMenuName = "Settings";
    [SerializeField]
    private string storeMenuName = "Store";

    //Called when thr player wants to play the ice level
    public void loadIce()
    {
        SceneManager.LoadScene(iceLevelName);
    }

    //Called when thr player wants to play the city level
    public void loadCity()
    {
        SceneManager.LoadScene(cityLevelName);
    }

    //Called when thr player wants to view the main menu
    public void loadMain()
    {
        SceneManager.LoadScene(mainMenuLevelName);
    }

    //Called when the player wants to view the level select menu
    public void loadLevelSelect()
    {
        SceneManager.LoadScene(levelSelectMenuName);
    }

    //Called when the player wants to view the settings menu
    public void loadSettings()
    {
        SceneManager.LoadScene(settingsMenuName);
    }

    //Called when the player wants to view the settings menu
    public void loadStore()
    {
        SceneManager.LoadScene(storeMenuName);
    }
}
