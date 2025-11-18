using Cabarles_IPT.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cabarles_IPT.Domain.Queries
{
    public interface IGetAllOrderItemsQuery
    {
        Task<IEnumerable<OrderItem>> Execute();
    }
}
