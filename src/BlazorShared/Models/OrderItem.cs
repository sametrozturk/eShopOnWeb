namespace BlazorShared.Models;
public class OrderItem
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
