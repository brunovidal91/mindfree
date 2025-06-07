namespace MindFree.Models
{
    public class CategoryCompare: IComparable<CategoryCompare>
    {
        public string Title { get; set; } = string.Empty;
        public double Value { get; set; }
        public double Percentage { get; set; }


        public int CompareTo(CategoryCompare? other)
        {
            return Value.CompareTo(other?.Value);
        }
    }

    
}
