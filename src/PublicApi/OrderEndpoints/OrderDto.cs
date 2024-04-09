using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

}
