using System;

namespace EcommerceApi.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateTeamDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTeamDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
    }
}