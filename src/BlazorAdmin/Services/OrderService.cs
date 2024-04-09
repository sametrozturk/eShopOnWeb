using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class OrderService : IOrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<CatalogItemService> _logger;

    public OrderService(ICatalogLookupDataService<CatalogBrand> brandService,
        ICatalogLookupDataService<CatalogType> typeService,
        HttpService httpService,
        ILogger<CatalogItemService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    //public async Task<Order> GetById(int id)
    //{
    //    var itemGetTask = _httpService.HttpGet<Order>($"order/{id}");
    //    await Task.WhenAll(itemGetTask);
    //    var order = itemGetTask.Result;
    //    return order;
    //}

    public async Task<Order> Aprrove(Order order)
    {
        var itemGetTask = _httpService.HttpPost<Order>($"orders", order);
        await Task.WhenAll(itemGetTask);
        var item = itemGetTask.Result;

        return item;
    }

    public async Task<Order> GetOrderItemsById(int id)
    {
        var itemGetTask = _httpService.HttpGet<Order>($"orders/{id}");
        await Task.WhenAll(itemGetTask);
        var order = itemGetTask.Result;

        return order;
    }

    public async Task<List<Order>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching catalog items from API.");

        var itemListTask = _httpService.HttpGet<PagedOrderResponse>($"orders?PageSize=10");
        await Task.WhenAll(itemListTask);
        var orders = itemListTask.Result.Orders;

        return orders;
    }

    public async Task<List<Order>> List()
    {
        _logger.LogInformation("Fetching catalog items from API.");

        var itemListTask = _httpService.HttpGet<PagedOrderResponse>($"orders");
        await Task.WhenAll(itemListTask);
        var orders = itemListTask.Result.Orders;

        return orders;
    }
}
