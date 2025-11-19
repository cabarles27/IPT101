using Cabarles_IPT.Domain.Commands;
using Cabarles_IPT.Domain.Models;
using Cabarles_IPT.Framework.DbContextFactory;
using Cabarles_IPT.Framework.DTOs;
using System.Threading.Tasks;

namespace Cabarles_IPT.Framework.Commands
{
    public class CreateOrderItemCommand : ICreateOrderItemCommand
    {
        private readonly PosDbContextFactory _contextFactory;

        public CreateOrderItemCommand(PosDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<OrderItem> Execute(string itemName, int quantity, decimal pricePerItem)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var dto = new OrderItemDTO
                {
                    ItemName = itemName,
                    Quantity = quantity,
                    PricePerItem = pricePerItem
                };

                context.OrderItems.Add(dto);
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
