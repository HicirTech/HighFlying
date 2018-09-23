using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class HealthSystemTest {

    [Test]
    public void testEnable()
    {
        GameObject testObject = new GameObject();
        testObject.AddComponent<HealthSystem>();

        HealthSystem testSystem = testObject.GetComponent<HealthSystem>();
        testSystem.enable = false;

        Assert.IsFalse(testSystem.enable);

    }

    [Test]
    public void testInitialInvincability()
    {
            GameObject testObject = new GameObject();
            testObject.AddComponent<HealthSystem>();

            HealthSystem testSystem = testObject.GetComponent<HealthSystem>();
            testSystem.initialInvincCount = 1;
            testSystem.initialInvinc();
            testSystem.initialInvinc();

            Assert.AreEqual(testSystem.initialInvincCount, -1);
            Assert.IsFalse(testSystem.invincability);
    }
}
