namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderItemDto
{
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public decimal TotalAmount { get => Amount(); }

    public decimal Amount()
    {
        return UnitPrice * Units;
    }
}
