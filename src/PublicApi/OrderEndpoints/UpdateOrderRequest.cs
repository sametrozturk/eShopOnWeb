namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    public int Id { get; init; }

    public UpdateOrderRequest(int id)
    {
        Id = id;
    }
}
