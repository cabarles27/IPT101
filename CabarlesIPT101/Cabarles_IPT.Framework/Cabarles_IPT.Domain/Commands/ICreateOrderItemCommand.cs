using Cabarles_IPT.Domain.Models;
using System.Threading.Tasks;

namespace Cabarles_IPT.Domain.Commands
{
    public interface ICreateOrderItemCommand
    {
        Task<OrderItem> Execute(string itemName, int quantity, decimal pricePerItem);
    }
}
