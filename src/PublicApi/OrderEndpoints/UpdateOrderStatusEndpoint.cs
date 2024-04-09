using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

/// <summary>
/// Updates a Catalog Item
/// </summary>
public class UpdateOrderStatusEndpoint : IEndpoint<IResult, UpdateOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public UpdateOrderStatusEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/orders",
            async
            (UpdateOrderRequest request, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<Order>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var existingItem = await itemRepository.GetByIdAsync(request.Id);
        if (existingItem == null)
        {
            return Results.NotFound();
        }

        if (existingItem.Status == OrderStatusConstants.Approved)
        {
            throw new InvalidOperationException("The order is already approved.");
        }

        existingItem.SetStatus(OrderStatusConstants.Approved);

        await itemRepository.UpdateAsync(existingItem);

        response.Order = _mapper.Map<OrderDto>(existingItem);

        return Results.Ok(response.Order);
    }
}
