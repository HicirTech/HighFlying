
using UnityEngine;

public class CharacterMovement
{
    public Transform Character { get; private set; }
    public float MaxXTopMovement { get; private set; }
    public float MaxXBottomMovement { get; private set; }
    public float MaxYTopMovement { get; private set; }
    public float MaxYBottomMovement { get; private set; }
    public CharacterMovement(Transform character)
    {
        this.Character = character;
    }

    public void SetMaxMovement(float maxXTop, float maxXBottom, float maxYTop, float maxYBottom)
    {
        MaxXTopMovement = maxXTop;
        MaxXBottomMovement = maxXBottom;
        MaxYTopMovement = maxYTop;
        MaxYBottomMovement = maxYBottom;
    }

    /// <summary>
    /// move ship directly vector(xOffset, yOffset)
    /// beside that, check that move is valid or not
    /// </summary>
    /// <param name="xOffSet"></param>
    /// <param name="yOffSet"></param>
    /// <param name="isValidUpdate"></param>
    public void UpdatePosition(float xOffSet, float yOffSet, ref bool isValidUpdate)
    {
        isValidUpdate = true;

        float yCurrent = Character.localPosition.y + yOffSet;
        float xCurrent = Character.localPosition.x + xOffSet;
        float rowY = Mathf.Clamp(yCurrent, MaxYBottomMovement, MaxYTopMovement);//limited the x y way can go
        float rowX = Mathf.Clamp(xCurrent, MaxXBottomMovement, MaxXTopMovement);//limited the x y way can go

        Character.localPosition = new Vector3(rowX, rowY, Character.localPosition.z);
        if (rowX != xCurrent || rowY != yCurrent)
            isValidUpdate = false;
    }

}
