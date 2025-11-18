using System.Threading.Tasks;

namespace Cabarles_IPT.Domain.Commands
{
    public interface IDeleteOrderItemCommand
    {
        Task Execute(int id);
    }
}
