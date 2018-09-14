using UnityEngine;
using NUnit.Framework;

public class CharacterTest {
    
    public class CharacterPositionProperty
    {
        [TestCase(3, 3)]
        public void UpdatePositionTest(float moveX, float moveY)
        {
            var expectedPosition = new Vector3(2, 2, 0);
            var expectedValidPosition = false;
            var isValidPosition = true;
            var character = new GameObject().GetComponent<Transform>();
            var characterMovement = new CharacterMovement(character);
            characterMovement.SetMaxMovement(2, 2);
            characterMovement.UpdatePosition(new Vector3(moveX, moveY, 0), ref isValidPosition);
            Assert.That(expectedPosition == characterMovement.Character.localPosition);
            Assert.That(expectedValidPosition = isValidPosition);
            
        }
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
