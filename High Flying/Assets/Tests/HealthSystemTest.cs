using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class HealthSystemTest {

    private GameObject character;

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator HealthSystemTestWithEnumeratorPasses() {
        character = GameObject.Find("Character");
        HealthSystem hSys = character.GetComponent(typeof(HealthSystem)) as HealthSystem;
        
        hSys.healthUI = null;
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(hSys.enable, false);
    }
}
