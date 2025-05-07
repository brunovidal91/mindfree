using System.ComponentModel.DataAnnotations;

namespace MindFree.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Title { get; set; }
        public bool IsIncome { get; set; }
        public bool IsMonthly { get; set; }
        [MaxLength(1, ErrorMessage = "A repetição precisa ser d, m ou a.")]
        public string? Day { get; set; }
        public double Amount { get; set; } = 0;
    }
}
