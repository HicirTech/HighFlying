using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.TestTools;

public class PlayAgainTest
{
    public const string scenePath = "Assets/Scenes/CityLevel/CityLevel.unity";
    [UnityTest]
    public IEnumerator PlayAgainButtonTest()
    {
        var sceneAsync = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        var sceneAsyncName = sceneAsync.name;
        Scene scenePlayAgain = new Scene();
        while (!sceneAsync.isLoaded)
        {
            yield return null;
        }
        var buttonList = GameObject.Find("IngameUI").GetComponentsInChildren<Button>(true);
        Button buttonPlayAgain = null;
        foreach (Button btn in buttonList)
        {
            if (btn.name == "BackToPlayAgainButton")
            {
                buttonPlayAgain = btn;
                buttonPlayAgain.onClick.AddListener(() =>
                {
                    scenePlayAgain = EditorSceneManager.OpenScene(scenePath);
                    Debug.Log(sceneAsyncName + " " + scenePlayAgain.name);
                    Assert.That(sceneAsyncName == scenePlayAgain.name, "Play again not the same scene");
                });
            }
        }
        
        Assert.That(buttonPlayAgain != null, "Can't find Button Play Again");
        buttonPlayAgain.onClick.Invoke();
        
    }
}