using UnityEngine.UI;

public class Instruction
{
    public Text Description { get; private set; }
    public InstructionData Data { get; private set; }

    public Instruction(Text description, InstructionData data)
    {
        this.Description = description;
        this.Data = data;
    }

    /// <summary>
    ///  return value of call back in onValueChanged is float. but we always set is interger.
    ///  so no problem if level param is float
    /// </summary>
    /// <param name="level"></param>
    public void UpdateDescription(float level)
    {
        if (level > 0 && level <= 5)
            Description.text = Data.GetDescription((int)level - 1);
        else
            Description.text = "";
    }
}
