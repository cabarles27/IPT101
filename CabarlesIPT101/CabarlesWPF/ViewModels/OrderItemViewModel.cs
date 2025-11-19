using Cabarles_IPT.Domain.Models;
using System.ComponentModel;

namespace CabarlesWPF.ViewModels
{
    public class OrderItemViewModel : ViewModelBase
    {
        private readonly OrderItem _orderItem;

        public OrderItemViewModel(OrderItem orderItem)
        {
            _orderItem = orderItem;
        }

        public int Id => _orderItem.Id;

        public string ItemName
        {
            get => _orderItem.ItemName;
            set
            {
                _orderItem.ItemName = value;
                OnPropertyChanged(nameof(ItemName));
                OnPropertyChanged(nameof(Total));
            }
        }

        public int Quantity
        {
            get => _orderItem.Quantity;
            set
            {
                _orderItem.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Total));
            }
        }

        public decimal PricePerItem
        {
            get => _orderItem.PricePerItem;
            set
            {
                _orderItem.PricePerItem = value;
                OnPropertyChanged(nameof(PricePerItem));
                OnPropertyChanged(nameof(Total));
            }
        }

        public decimal Total => _orderItem.Total;

        public OrderItem GetOrderItem() => _orderItem;
    }
}
