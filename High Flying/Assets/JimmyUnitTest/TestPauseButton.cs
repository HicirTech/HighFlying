using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestPauseButton {

    [Test]
    public void TestPauseButtonSimplePasses() {
        // Use the Assert class to test conditions.
      
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator TestPauseButtonWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        GameObject pauseButton = GameObject.FindWithTag("PauseButton");
        if (pauseButton.activeSelf) // if pause Button active
        {
            Assert.AreEqual(0, Time.timeScale);
        }
        else
        {
            Assert.AreEqual(1, Time.timeScale);
        }
        yield return null;
    }
}
