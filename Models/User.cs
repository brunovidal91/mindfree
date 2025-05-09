using System.ComponentModel.DataAnnotations;

namespace MindFree.Models
{
    public class User
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Informe o e-mail")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? Name { get; set; }
        public string? CreatedAt { get; set; }
        public bool Admin { get; set; }

    }
}
