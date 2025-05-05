using System.ComponentModel.DataAnnotations;

namespace MindFree.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsIncome { get; set; }
        public bool IsRecurring { get; set; }
        [MaxLength(1, ErrorMessage = "A repetição precisa ser d, m ou a.")]
        public char? RepeatPer{ get; set; } //d - m - y
        public string? Day { get; set; }
        public double? Amount { get; set; }
    }
}
