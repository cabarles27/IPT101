using Cabarles_IPT.Domain.Models;
using Cabarles_IPT.Domain.Queries;
using Cabarles_IPT.Framework.DbContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cabarles_IPT.Framework.Queries
{
    public class GetAllOrderItemsQuery : IGetAllOrderItemsQuery
    {
        private readonly PosDbContextFactory _contextFactory;

        public GetAllOrderItemsQuery(PosDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<OrderItem>> Execute()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var dtos = await context.OrderItems.ToListAsync();
                
                return dtos.Select(dto => new OrderItem
                {
                    Id = dto.Id,
                    ItemName = dto.ItemName,
                    Quantity = dto.Quantity,
                    PricePerItem = dto.PricePerItem
                }).ToList();
            }
        }
    }
}
