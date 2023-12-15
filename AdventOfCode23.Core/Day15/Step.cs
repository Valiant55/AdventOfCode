namespace AdventOfCode23.Core.Day15;

public enum Instruction
{ 
    REMOVE_LENS,
    ADD_LENS,
    INVALID
}

public class Step
{
    public string Value { get; set; }
    public string Label { get; set; }
    public int HashValue { get; set; }
    public Instruction Instruction { get; set; }
    public int FocalLength { get; set; }

    public Step(string value)
    {
        Value = value;

        if (value.Contains('='))
        {
            var split = value.Split('=');
            Label = split[0];
            HashValue = split[0].ApplyHASH();
            Instruction = Instruction.ADD_LENS;
            FocalLength = int.Parse(split[1]);
        }
        else if(value.Contains('-'))
        {
            var split = value.Split('-');
            Label = split[0];
            HashValue = split[0].ApplyHASH();
            Instruction = Instruction.REMOVE_LENS;
            FocalLength = 0;
        }
        else
        {
            Label = string.Empty;
            HashValue = 0;
            Instruction = Instruction.INVALID;
            FocalLength = 0;
        }
    }
}
