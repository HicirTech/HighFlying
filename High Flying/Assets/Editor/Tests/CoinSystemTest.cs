using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CoinSystemTest {
    [Test]
    public void testEnable()
    {
        GameObject testObject = new GameObject();
        testObject.AddComponent<CoinSystem>();

        CoinSystem testSystem = testObject.GetComponent<CoinSystem>();
        testSystem.enable = false;

        Assert.IsFalse(testSystem.enable);
    }
    
}
