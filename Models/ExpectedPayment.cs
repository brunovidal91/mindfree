namespace MindFree.Models
{
    public class ExpectedPayment: IComparable<ExpectedPayment>
    {
        public string Name { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public double Value { get; set; }
        public Transaction Transaction { get; set; }


        public ExpectedPayment()
        {
            Transaction = new Transaction();
        }


        public int CompareTo(ExpectedPayment? other)
        {
            return int.Parse(Date).CompareTo(int.Parse(other?.Date));
        }
    }
}
