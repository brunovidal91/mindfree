using System.ComponentModel.DataAnnotations;

namespace MindFree.Models
{
    public class UsersRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public bool Admin { get; set; } = false;
    }
}
