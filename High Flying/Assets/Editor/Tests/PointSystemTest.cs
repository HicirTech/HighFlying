using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PointSystemTest {

    [Test]
    public void testEnable()
    {
        GameObject testObject = new GameObject();
        testObject.AddComponent<PointSystem>();

        PointSystem testSystem = testObject.GetComponent<PointSystem>();
        testSystem.enable = false;

        Assert.IsFalse(testSystem.enable);

    }

    [Test]
    public void testCalculator()
    {
        //points = Mathf.Round((((coinsCollectedCounter+1)*coinsCollMult)*(ringsPassedCounter*ringsPassMult))*Mathf.Pow((pointRingCounter*pointRMult), difficulty));

        GameObject testObject = new GameObject();
        testObject.AddComponent<PointSystem>();

        PointSystem testSystem = testObject.GetComponent<PointSystem>();
        testSystem.setPoints(0);

        testSystem.setCoinsCollectedCounter(1);
        testSystem.setRingsPassedCounter(1);
        testSystem.setPointRingCounter(1);
        testSystem.setDifficulty(1);

        testSystem.calculatePoints();

        Assert.AreNotEqual(testSystem.getPoints(), 0);
        Assert.AreEqual(12.0f, testSystem.getPoints()); //Expected points with values set above
    }

    
}
