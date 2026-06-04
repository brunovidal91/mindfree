using System.Runtime.CompilerServices;
using MindFree.Utils;

namespace MindFree.Models
{
    public class MonthResult
    {
        public int MonthIndex { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public double Expenses { get; set; }
        public double Incomes { get; set; }
        public double Balance { get; set; }
        public bool IsClosed { get; set; } = false;
    }
}
