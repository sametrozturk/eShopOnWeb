using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetByIdEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderGetByIdEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/{orderId}",
            async (int orderId, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new GetByIdOrderRequest(orderId), itemRepository);
            })
            .Produces<OrderDto>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> itemRepository)
    {
        await Task.Delay(1000);
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var orderSpec = new OrderWithItemsByIdSpec(request.OrderId);

        var order = await itemRepository.GetBySpecAsync(orderSpec);

        return Results.Ok(_mapper.Map<OrderDto>(order));
    }
}
