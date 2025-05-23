using System;

namespace EcommerceApi.DTOs
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OpeningHours { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateRestaurantDto
    {
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OpeningHours { get; set; }
    }

    public class UpdateRestaurantDto
    {
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OpeningHours { get; set; }
    }
}