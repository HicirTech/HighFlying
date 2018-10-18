using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.EventSystems;

public class MoveLeftRightButtonTest
{
    
    public const string cityLevel = "Assets/Scenes/CityLevel/City Level.unity";
    public const string iceLevel = "Assets/Scenes/CityLevel/City Level.unity";
    [UnityTest()]
    public IEnumerator CityLevelTest()
    {
        var sceneAsync = EditorSceneManager.OpenScene(cityLevel, OpenSceneMode.Single);
        while (!sceneAsync.isLoaded)
        {
            yield return null;
        }
        var uiEventTrigger = GameObject.Find("IngameUI").GetComponentsInChildren<EventTrigger>(true);
        Assert.That(uiEventTrigger != null && uiEventTrigger.Length == 2
            && uiEventTrigger[0].name.Equals("MoveRightButton") && uiEventTrigger[1].name.Equals("MoveLeftButton"), "Can't find GameOver");
    }

    [UnityTest()]
    public IEnumerator IceLevelTest()
    {
        var sceneAsync = EditorSceneManager.OpenScene(iceLevel, OpenSceneMode.Single);
        while (!sceneAsync.isLoaded)
        {
            yield return null;
        }
        var uiEventTrigger = GameObject.Find("IngameUI").GetComponentsInChildren<EventTrigger>(true);
        Assert.That(uiEventTrigger != null && uiEventTrigger.Length == 2
            && uiEventTrigger[0].name.Equals("MoveRightButton") && uiEventTrigger[1].name.Equals("MoveLeftButton"), "Can't find GameOver");
    }
}
