namespace MindFree.Models
{
    public class Result
    {
        public string Month { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public double Incomes { get; set; }
        public double Expenses { get; set; }
        public bool Closed { get; set; } = false;
    }
    
}
