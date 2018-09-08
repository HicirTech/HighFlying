using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class HealthSystemTest {

    private GameObject character;
    private HealthSystem hSys;
    private int healthBeforeCollision;
    private int healthAfterCollision;

    [Test]
    public void HealthSystemTestSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator HealthSystemTestWithEnumeratorPasses() {
        character = GameObject.Find("Character");
        hSys = character.GetComponent<HealthSystem>();
        healthBeforeCollision = hSys.health;

        yield return new WaitForSeconds(10);

        healthAfterCollision = hSys.health;

        Assert.AreNotEqual(healthBeforeCollision, healthAfterCollision);
    }
}
