using Cabarles_IPT.Domain.Commands;
using Cabarles_IPT.Domain.Models;
using Cabarles_IPT.Framework.DbContextFactory;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cabarles_IPT.Framework.Commands
{
    public class UpdateOrderItemCommand : IUpdateOrderItemCommand
    {
        private readonly PosDbContextFactory _contextFactory;

        public UpdateOrderItemCommand(PosDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<OrderItem> Execute(int id, string itemName, int quantity, decimal pricePerItem)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var dto = await context.OrderItems.FindAsync(id);
                if (dto == null)
                    return null;

                dto.ItemName = itemName;
                dto.Quantity = quantity;
                dto.PricePerItem = pricePerItem;

                await context.SaveChangesAsync();

                return new OrderItem
                {
                    Id = dto.Id,
                    ItemName = dto.ItemName,
                    Quantity = dto.Quantity,
                    PricePerItem = dto.PricePerItem
                };
            }
        }
    }
}
