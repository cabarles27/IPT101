using Cabarles_IPT.Domain.Commands;
using Cabarles_IPT.Framework.DbContextFactory;
using System.Threading.Tasks;

namespace Cabarles_IPT.Framework.Commands
{
    public class DeleteOrderItemCommand : IDeleteOrderItemCommand
    {
        private readonly PosDbContextFactory _contextFactory;

        public DeleteOrderItemCommand(PosDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var dto = await context.OrderItems.FindAsync(id);
                if (dto != null)
                {
                    context.OrderItems.Remove(dto);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
