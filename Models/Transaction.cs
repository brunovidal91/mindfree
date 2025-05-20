namespace MindFree.Models
{
    public class Transaction
    {
        public string Id { get; set; } = string.Empty;
        public double Value { get; set; }
        public Category? Category { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;

        public Transaction()
        {
            Category = new Category();
        }
    }
}
