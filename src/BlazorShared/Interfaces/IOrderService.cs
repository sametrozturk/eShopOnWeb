using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface IOrderService
{
    //Task<Order> GetById(int id);
    Task<Order> Aprrove(Order order);
    Task<Order> GetOrderItemsById(int id);
    Task<List<Order>> ListPaged(int pageSize);
    Task<List<Order>> List();
}
