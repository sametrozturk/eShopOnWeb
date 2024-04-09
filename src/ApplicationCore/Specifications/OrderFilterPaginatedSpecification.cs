using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderFilterPaginatedSpecification : Specification<Order>
{
    public OrderFilterPaginatedSpecification(int skip, int take)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered)
            .Skip(skip).Take(take);
    }
}
