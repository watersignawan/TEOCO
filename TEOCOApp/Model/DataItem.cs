namespace TEOCOApp.Model
{
    public class DataItem
    {
        public DataItem(DataTimeSpan timespan, int dataset, int value)
        {
            this.Timespan = timespan;
            this.Dataset = dataset;
            this.Value = value;
        }

        public DataTimeSpan Timespan { get; private set; }

        public int Dataset { get; private set; }

        public int Value { get; private set; }

        public override string ToString()
        {
            return $"{Timespan}, {Dataset}, {Value}";
        }
    }
}
