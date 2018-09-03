using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    [SerializeField]
    private string iceLevelName = "Ice Level";
    [SerializeField]
    private string cityLevelName = "City Level";
    [SerializeField]
    private string mainMenuLevelName = "MainPlay";

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
}
