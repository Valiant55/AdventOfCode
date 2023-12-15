namespace AdventOfCode23.Core.Day15;

public class BoxArray
{
    public List<List<Lens>> Boxes { get; set; }

    public class Lens
    {
        public string Label { get; set; }
        public int FocalLength { get; set; }
    }

    public BoxArray()
    {
        Boxes = new();

        for(int i = 0; i < 256; i++)
        {
            Boxes.Add(new List<Lens>());
        }
    }

    public void BuildArray(List<Step> steps)
    {
        foreach(var step in steps)
        {
            if(step.Instruction == Instruction.ADD_LENS)
            {
                var lens = Boxes[step.HashValue].Where(l => l.Label == step.Label).FirstOrDefault();
                if(lens != null)
                {
                    lens.FocalLength = step.FocalLength;
                }
                else
                {
                    Boxes[step.HashValue].Add(new Lens() { Label = step.Label, FocalLength = step.FocalLength });
                }
            }
            else if(step.Instruction == Instruction.REMOVE_LENS)
            {
                var lens = Boxes[step.HashValue].Where(l => l.Label == step.Label).FirstOrDefault();
                if(lens != null) Boxes[step.HashValue].Remove(lens);
            }
        }
    }

    public long CalculateFocusingPower()
    {
        long result = 0;

        foreach((var box, var b )in Boxes.Select((b, i) => (b, i)))
        {
            long value = 0;

            foreach((var lens, var l) in box.Select((b, i) => (b, i)))
            {
                value += (b + 1) * (l + 1) * lens.FocalLength;
            }

            result += value;
        }

        return result;
    }
}
