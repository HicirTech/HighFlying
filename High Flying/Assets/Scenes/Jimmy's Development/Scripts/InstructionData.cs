using UnityEngine;

[CreateAssetMenu(fileName = "Instruction Data", menuName = "Scriptable/InstructionData", order = 1)]
public class InstructionData : ScriptableObject {
    // contain description of each difficult rating
    [SerializeField]
    private string[] description;

    public string GetDescription(int index)
    {
        if (index < description.Length)
        {
            return description[index];
        }
        else
        {
            return string.Empty;
        }
    }
}
