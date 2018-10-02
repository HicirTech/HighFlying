using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.TestTools;

public class EndGameInfoTest
{
    
    public const string scenePath = "Assets/Scenes/CityLevel/City Level.unity";
    [UnityTest]
    public IEnumerator EndGameInfo()
    {
        var sceneAsync = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        while (!sceneAsync.isLoaded)
        {
            yield return null;
        }
        var uiGameOver = GameObject.Find("IngameUI").GetComponentInChildren<GameOver>(true);
        Assert.That(uiGameOver != null, "Can't find GameOver");

        uiGameOver.LevelComplete(1, 1, 1, 1, 1);

        var textList = uiGameOver.gameObject.GetComponentsInChildren<Text>(true);
        Text textCoinCollected = null;
        foreach (Text txt in textList)
        {
            if (txt.name == "NumberCoins")
            {
                textCoinCollected = txt;
                break;
            }
        }
        Assert.That(textCoinCollected.text == "1/20", "Result is wrong! Good result is 1/20");
    }
}
