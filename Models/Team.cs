using System;

namespace EcommerceApi.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}