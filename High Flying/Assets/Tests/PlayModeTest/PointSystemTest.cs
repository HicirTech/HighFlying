using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PointSystemTest
{

    GameObject character;
    //PointSystem pSys;
    Component[] listOfComponents;
    float pointsBeforeEngage;
    float pointsAfterEngage;

    [Test]
    public void PointSystemTestSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PointSystemTestWithEnumeratorPasses() {
        character = GameObject.Find("Character");
        listOfComponents = character.GetComponents(typeof(Component));

        foreach (Component c in listOfComponents)
        {
            Debug.Log("Component: " + c);
        }

       // pSys = character.GetComponent<PointSystem>();
        pointsBeforeEngage = pSys.points;

        yield return new WaitForSeconds(5);

        pointsAfterEngage = pSys.points;

        Assert.AreNotEqual(pointsBeforeEngage, pointsAfterEngage);
    }
}
