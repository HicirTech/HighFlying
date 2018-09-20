using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEditor;

public class CharacterTest
{

    public class CharacterPositionProperty
    {
        [TestCase(3, 3, 2, 2, false)]
        [TestCase(2, 2, 2, 2, true)]
        [TestCase(-2, 1, -2, 1, true)]
        [TestCase(-3, 3, -2, 2, false)]
        public void UpdatePositionTest(float xOffset, float yOffset, float expectedPositionX, float expectedPositionY, bool expectedValidPosition)
        {
            var expectedPosition = new Vector3(expectedPositionX, expectedPositionY, 0);
            var isValidPosition = true;
            var character = new GameObject().GetComponent<Transform>();
            var characterMovement = new CharacterMovement(character);
            characterMovement.SetMaxMovement(2, -2, 2, -2);
            characterMovement.UpdatePosition(xOffset, yOffset, ref isValidPosition);
            Assert.That(expectedPosition == characterMovement.Character.localPosition);
            Assert.That(expectedValidPosition == isValidPosition);
        }
    }
}
