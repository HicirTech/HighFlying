using UnityEngine;
using UnityEngine.UI;

public class UIInstruction : MonoBehaviour {

    [Tooltip("Drag slider level here")][SerializeField]
    private Slider sldLevel;
    [Tooltip("Drag text here, it will updates info of difficult level")][SerializeField]
    private Text txtContent;
    [Tooltip("Data contain description of each difficult level")][SerializeField]
    private InstructionData data;

    private Instruction instruction;

    private void Start()
    {
        InitInstruction();

        sldLevel.onValueChanged.AddListener(instruction.UpdateDescription);
        instruction.UpdateDescription(sldLevel.value);
    }

    /// <summary>
    /// declare and create new objec for instruction
    /// </summary>
    private void InitInstruction()
    {
        instruction = new Instruction(txtContent, data);
    }
}
