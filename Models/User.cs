﻿namespace MindFree.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? Name { get; set; }
        public string? CreatedAt { get; set; }
        public bool Admin { get; set; }

    }
}
