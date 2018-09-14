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
        public void UpdatePositionTest(float moveX, float moveY, float expectedPositionX, float expectedPositionY, bool expectedValidPosition)
        {
            var expectedPosition = new Vector3(expectedPositionX, expectedPositionY, 0);
            var isValidPosition = true;
            var character = new GameObject().GetComponent<Transform>();
            var characterMovement = new CharacterMovement(character);
            characterMovement.SetMaxMovement(2, 2);
            characterMovement.UpdatePosition(new Vector3(moveX, moveY, 0), ref isValidPosition);
            Assert.That(expectedPosition == characterMovement.Character.localPosition);
            Assert.That(expectedValidPosition == isValidPosition);
        }
    }
}

public class InstructionTest
{
    public class UpdateInstructionMethod
    {
        [TestCase(1, "Can I play daddy : Player will have 5 lives, and a speed of 3.3m/s. Estimate time to finish: 5 minutes")]
        [TestCase(2, "Don't hurt me: player will have 4 lives, and a speed of 5.5m/s. Estimate time to finish: 3 minutes")]
        [TestCase(3, "Normal: Player will have 3 lives, and a speed of 8.5m/s. Estimate time to finish: 1.9 minutes")]
        [TestCase(4, "Sweaty Boy: Player will have 3 lives, and a speed of 20m/s. Estimate time to finish: 50 seconds")]
        [TestCase(5, "Hell on Earth: Player will have 1 live, and a speed of 50m/s. Estimate time to finish: 20 seconds")]
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

public class Instruction
{
    public Text Description { get; private set; }
    public InstructionData Data { get; private set; }

    public Instruction(Text description, InstructionData data)
    {
        this.Description = description;
        this.Data = data;
    }

    public void UpdateDescription(float level)
    {
        if (level > 0 && level <= 5)
            Description.text = Data.GetDescription((int)level - 1);
        else
            Description.text = "";
    }
}

public class CharacterMovement
{
    public Transform Character { get; private set; }
    public float MaxXMovement { get; private set; }
    public float MaxYMovement { get; private set; }
    public CharacterMovement(Transform character)
    {
        this.Character = character;
    }

    public void SetMaxMovement(float maxX, float maxY)
    {
        MaxXMovement = maxX;
        MaxYMovement = maxY;
    }

    public void UpdatePosition(Vector3 direction, ref bool isValidUpdate)
    {
        isValidUpdate = true;

        Character.Translate(direction);

        float rowY = Mathf.Clamp(Character.localPosition.y, -MaxYMovement, MaxYMovement);//limited the x y way can go
        float rowX = Mathf.Clamp(Character.localPosition.x, -MaxXMovement, MaxXMovement);//limited the x y way can go

        if (rowX != direction.x || rowY != direction.y)
        {
            Character.localPosition = new Vector3(rowX, rowY, Character.localPosition.z);
            isValidUpdate = false;
        }
    }

}
