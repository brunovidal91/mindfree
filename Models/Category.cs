using System.ComponentModel.DataAnnotations;

namespace MindFree.Models
{
    public class Category
    {

        public string? id { get; set; }
        public string title { get; set; } = "";
        public bool isIncome { get; set; } = false;
        public bool isMonthly { get; set; } = false;
        public string day { get; set; } = "";
        public double amount { get; set; } = 0.00;
    }
}
