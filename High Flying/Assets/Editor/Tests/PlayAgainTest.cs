using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.TestTools;

public class PlayAgainTest
{
    public const string scenePath = "Assets/Scenes/CityLevel/City Level.unity"; // string for city level path
    [UnityTest]
    public IEnumerator PlayAgainButtonTest()
    {
        var sceneAsync = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single); //load city level scene
        var sceneAsyncName = sceneAsync.name; //get scene name
        Scene scenePlayAgain = new Scene(); 
        while (!sceneAsync.isLoaded) //if the scene cant be load, return null
        {
            yield return null;
        }
        var buttonList = GameObject.Find("IngameUI").GetComponentsInChildren<Button>(true); //get button array from InGameUi object
        Button buttonPlayAgain = null; //intilisise a button for buttonPlayAgain
        foreach (Button btn in buttonList) //find the button PlayAgain in buttonList array
        {
            if (btn.name == "BackToPlayAgainButton") //get the button play again
            {
                buttonPlayAgain = btn;
                buttonPlayAgain.onClick.AddListener(() => //if that button is clicked
                {
                    scenePlayAgain = EditorSceneManager.OpenScene(scenePath); //open the scene
                    Debug.Log(sceneAsyncName + " " + scenePlayAgain.name);
                    Assert.That(sceneAsyncName == scenePlayAgain.name, "Play again not the same scene"); //check that scenePlayAgain name with city level scene name
                });
            }
        }
        
        Assert.That(buttonPlayAgain != null, "Can't find Button Play Again"); //if that button is null mean cant find object
        buttonPlayAgain.onClick.Invoke(); //trigger the button
        
    }
}
