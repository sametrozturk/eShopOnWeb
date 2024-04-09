using System;
using System.Collections.Generic;

namespace BlazorShared.Models;

public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
