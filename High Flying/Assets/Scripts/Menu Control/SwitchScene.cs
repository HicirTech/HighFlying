using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    [SerializeField]
    private string iceLevelName = "Ice Level";
    [SerializeField]
    private string cityLevelName = "CityLevel";
    [SerializeField]
    private string mainMenuLevelName = "MainPlay";
    [SerializeField]
    private string levelSelectMenuName = "LevelSelect";
    [SerializeField]
    private string settingsMenuName = "Settings";
    [SerializeField]
    private string storeMenuName = "Store";

    public void loadIce()
    {
        SceneManager.LoadScene(iceLevelName);
    }

    public void loadCity()
    {
        SceneManager.LoadScene(cityLevelName);
    }

    public void loadMain()
    {
        SceneManager.LoadScene(mainMenuLevelName);
    }

    public void loadLevelSelect()
    {
        SceneManager.LoadScene(levelSelectMenuName);
    }

    public void loadSettings()
    {
        SceneManager.LoadScene(settingsMenuName);
    }

    public void loadStore()
    {
        SceneManager.LoadScene(storeMenuName);
    }
}
