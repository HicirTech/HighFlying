using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections;

public class FadingTest {

public const string scenePath = "Assets/Scenes/Zeting's development/Player Move development.unity";
    [UnityTest]
    public IEnumerator TestFade()
    {       
        var sceneAsync = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            
        Fade fadeControl = GameObject.Find("TestCube").GetComponent<Fade>();
        yield return null;
        Assert.NotNull(fadeControl);
        fadeControl.fadable=true;

        for(int i = 0 ;i!=120; i++)
        {
            fadeControl.EditFade();
            yield return null;
        }
        Assert.That(GameObject.Find("TestCube").GetComponent<Renderer>().sharedMaterial.color.a<0.9f);
    }  
    [UnityTest]
    public IEnumerator DestoryAfterFade()
    {       
        var sceneAsync = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            
        Fade fadeControl = GameObject.Find("TestCube").GetComponent<Fade>();
        yield return null;
        fadeControl.fadable=true;
        Color testColor = GameObject.Find("TestCube").GetComponent<Renderer>().sharedMaterial.color;
        while(GameObject.Find("TestCube")!=null)
        {
            fadeControl.EditFade();
            yield return null;
        }
        Assert.That(GameObject.Find("TestCube") == null);
    }
    [TearDown]
    public void recoverColor()
    {
       if (GameObject.Find("TestCube")!=null)
       {
        Color testColor = GameObject.Find("TestCube").GetComponent<Renderer>().sharedMaterial.color;
        testColor.a=1.0f;
        }
    }
}
