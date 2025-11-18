using System.Collections.ObjectModel;
using CabarlesWPF.ViewModels;

namespace CabarlesWPF.Stores
{
    public class OrderStore
    {
        public ObservableCollection<OrderItemViewModel> OrderItems { get; } = new ObservableCollection<OrderItemViewModel>();
    }
}
