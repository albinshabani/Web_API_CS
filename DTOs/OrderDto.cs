using System;
using System.Collections.Generic;

namespace EcommerceApi.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class UpdateOrderDto
    {
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
    }
}