using System;

namespace EcommerceApi.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OpeningHours { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}