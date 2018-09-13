using UnityEngine;
using UnityEngine.UI;

public class UIInstruction : MonoBehaviour {

    [Tooltip("Drag slider level here")][SerializeField]
    private Slider sldLevel;
    [Tooltip("Drag text here, it will updates info of difficult level")][SerializeField]
    private Text txtContent;
    [Tooltip("Data contain description of each difficult level")][SerializeField]
    private InstructionData data;

    private void Start()
    {
        sldLevel.onValueChanged.AddListener(UpdateDescription);
        UpdateDescription(sldLevel.value);
    }

    public void UpdateDescription(float level)
    {
        txtContent.text = data.GetDescription((int)level - 1);
    }
}
