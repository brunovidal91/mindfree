using System.ComponentModel.DataAnnotations;

namespace MindFree.Models
{
    public class User
    {
        public string? Id { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Name { get; set; }
        public string? CreatedAt { get; set; }
        public bool Admin { get; set; }

    }
}
