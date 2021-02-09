
namespace GA
{
    class SetPoint
    {
        public SetPoint(string name, double min, double max)
        {
            Name = name;
            Min = min;
            Max = max;
        }

        public string Name { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }


    }
}
