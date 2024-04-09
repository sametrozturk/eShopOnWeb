using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.Orders;

public partial class Orders : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderService OrderService { get; set; }

    private Details DetailsComponent { get; set; }

    private List<Order> _orders = new List<Order>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _orders = await OrderService.List();
            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }


    private async void DetailsClick(int id)
    {
        await DetailsComponent.Open(id);
    }

    private async Task ReloadCatalogItems()
    {
        _orders = await OrderService.List();
        StateHasChanged();
    }
}
