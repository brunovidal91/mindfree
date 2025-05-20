namespace MindFree.Models
{
    public class Month
    {
        public int MonthPosition { get; set; }
        public string? MonthName { get; set; }

        public Month() { }
        public Month(int monthPosition, string monthName)
        {
            MonthPosition = monthPosition;
            MonthName = monthName;
        }
    }
}
