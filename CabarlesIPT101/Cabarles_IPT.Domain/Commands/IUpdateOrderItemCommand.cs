using Cabarles_IPT.Domain.Models;
using System.Threading.Tasks;

namespace Cabarles_IPT.Domain.Commands
{
    public interface IUpdateOrderItemCommand
    {
        Task<OrderItem> Execute(int id, string itemName, int quantity, decimal pricePerItem);
    }
}
