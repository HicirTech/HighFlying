using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTest
{
    public class UpdateInstructionMethod
    {
        [TestCase(1, "Can I play, Daddy: Player will have 5 lives, and a speed of 3.3m/s. Estimate time to finish: 5 minutes")]
        [TestCase(2, "Don't hurt me: player will have 4 lives, and a speed of 5.5m/s. Estimate time to finish: 3 minutes")]
        [TestCase(3, "Normal: Player will have 3 lives, and a speed of 8.5m/s. Estimate time to finish: 1.9 minutes")]
        [TestCase(4, "Sweaty Boy: Player will have 3 lives, and a speed of 20m/s. Estimate time to finish: 50 seconds")]
        [TestCase(5, "Hell on Earth: Player will have 1 life, and a speed of 50m/s. Estimate time to finish: 20 seconds")]
        [TestCase(-1, "")]
        public void UpdateDescriptionTest(float level, string expectedResult)
        {
            var data = (InstructionData)AssetDatabase.LoadAssetAtPath("Assets/Scenes/Jimmy's Development/Scriptables/InstructionData.asset", typeof(InstructionData));
            var description = new GameObject().AddComponent<Text>();

            var instruction = new Instruction(description, data);

            instruction.UpdateDescription(level);

            Assert.That(instruction.Description.text == expectedResult);
        }
    }
}