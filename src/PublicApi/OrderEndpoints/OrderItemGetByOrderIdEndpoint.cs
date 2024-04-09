
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using global::Microsoft.AspNetCore.Routing;
using global::Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using global::Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

/// <summary>
/// Get a Catalog Item by Id
/// </summary>
public class OrderItemGetByOrderIdEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderItemGetByOrderIdEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items/{orderId}",
            async (int orderId, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new GetByIdOrderRequest(orderId), itemRepository);
            })
            .Produces<Order>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var orderSpec = new OrderWithItemsByIdSpec(request.OrderId);

        var order = await itemRepository.GetBySpecAsync(orderSpec);

        if (order == null)
        {
            return Results.NotFound();
        }

        var items = order.OrderItems.ToList();

        var orderDto = _mapper.Map<OrderDto>(order);
        orderDto.OrderItems = _mapper.Map<List<OrderItemDto>>(items);

        return Results.Ok(response.Order);
    }
}
