using Cabarles_IPT.Domain.Commands;
using Cabarles_IPT.Domain.Queries;
using CabarlesWPF.Commands;
using CabarlesWPF.Stores;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CabarlesWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly OrderStore _orderStore;
        private readonly ICreateOrderItemCommand _createCommand;
        private readonly IUpdateOrderItemCommand _updateCommand;
        private readonly IDeleteOrderItemCommand _deleteCommand;
        private readonly IGetAllOrderItemsQuery _getAllQuery;

        private string _selectedItemName = "Coffee";
        private string _quantity = "";
        private string _pricePerItem = "";
        private OrderItemViewModel _selectedOrderItem;
        private bool _isEditMode = false;
        private int _editingItemId = 0;

        public MainViewModel(
            OrderStore orderStore,
            ICreateOrderItemCommand createCommand,
            IUpdateOrderItemCommand updateCommand,
            IDeleteOrderItemCommand deleteCommand,
            IGetAllOrderItemsQuery getAllQuery)
        {
            _orderStore = orderStore;
            _createCommand = createCommand;
            _updateCommand = updateCommand;
            _deleteCommand = deleteCommand;
            _getAllQuery = getAllQuery;

            OrderItems = new ObservableCollection<OrderItemViewModel>();

            SaveCommand = new AsyncRelayCommand(Save, CanSave);
            EditItemCommand = new RelayCommand<OrderItemViewModel>(EditItem, CanEditItem);
            DeleteItemCommand = new AsyncRelayCommand<OrderItemViewModel>(DeleteItem, CanDeleteItem);
            ClearFormCommand = new RelayCommand(ClearForm);
            NewOrderCommand = new AsyncRelayCommand(NewOrder);
            PayCommand = new RelayCommand(Pay, CanPay);
            CancelCommand = new RelayCommand(Cancel, CanCancel);

            LoadOrderItems();
        }

        public ObservableCollection<OrderItemViewModel> OrderItems { get; }

        public string SelectedItemName
        {
            get => _selectedItemName;
            set
            {
                _selectedItemName = value;
                OnPropertyChanged(nameof(SelectedItemName));
            }
        }

        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string PricePerItem
        {
            get => _pricePerItem;
            set
            {
                _pricePerItem = value;
                OnPropertyChanged(nameof(PricePerItem));
            }
        }

        public OrderItemViewModel SelectedOrderItem
        {
            get => _selectedOrderItem;
            set
            {
                _selectedOrderItem = value;
                OnPropertyChanged(nameof(SelectedOrderItem));
            }
        }

        public decimal TotalAmount => OrderItems.Sum(item => item.Total);

        public string FormTitle => _isEditMode ? "Edit Item" : "Add Item to Cart";
        public string SaveButtonText => _isEditMode ? "Update" : "Add to Cart";

        public ICommand SaveCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand ClearFormCommand { get; }
        public ICommand NewOrderCommand { get; }
        public ICommand PayCommand { get; }
        public ICommand CancelCommand { get; }

        private async void LoadOrderItems()
        {
            var items = await _getAllQuery.Execute();
            OrderItems.Clear();
            foreach (var item in items)
            {
                OrderItems.Add(new OrderItemViewModel(item));
            }
            OnPropertyChanged(nameof(TotalAmount));
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(SelectedItemName) &&
                   int.TryParse(Quantity, out int qty) && qty > 0 &&
                   decimal.TryParse(PricePerItem, out decimal price) && price > 0;
        }

        private async System.Threading.Tasks.Task Save()
        {
            if (int.TryParse(Quantity, out int qty) && decimal.TryParse(PricePerItem, out decimal price))
            {
                if (_isEditMode)
                {
                    // Update existing item
                    var updatedItem = await _updateCommand.Execute(_editingItemId, SelectedItemName, qty, price);
                    var existingItem = OrderItems.FirstOrDefault(x => x.Id == _editingItemId);
                    if (existingItem != null)
                    {
                        var index = OrderItems.IndexOf(existingItem);
                        OrderItems[index] = new OrderItemViewModel(updatedItem);
                    }
                }
                else
                {
                    // Add new item
                    var orderItem = await _createCommand.Execute(SelectedItemName, qty, price);
                    OrderItems.Add(new OrderItemViewModel(orderItem));
                }
                
                ClearForm();
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        private bool CanEditItem(OrderItemViewModel item)
        {
            return item != null;
        }

        private void EditItem(OrderItemViewModel item)
        {
            if (item != null)
            {
                _isEditMode = true;
                _editingItemId = item.Id;
                SelectedItemName = item.ItemName;
                Quantity = item.Quantity.ToString();
                PricePerItem = item.PricePerItem.ToString();
                
                OnPropertyChanged(nameof(FormTitle));
                OnPropertyChanged(nameof(SaveButtonText));
            }
        }

        private void ClearForm()
        {
            _isEditMode = false;
            _editingItemId = 0;
            Quantity = "";
            PricePerItem = "";
            SelectedItemName = "Coffee";
            
            OnPropertyChanged(nameof(FormTitle));
            OnPropertyChanged(nameof(SaveButtonText));
        }

        private bool CanDeleteItem(OrderItemViewModel item)
        {
            return item != null;
        }

        private async System.Threading.Tasks.Task DeleteItem(OrderItemViewModel item)
        {
            if (item != null)
            {
                var result = MessageBox.Show($"Delete {item.ItemName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await _deleteCommand.Execute(item.Id);
                    OrderItems.Remove(item);
                    OnPropertyChanged(nameof(TotalAmount));
                }
            }
        }

        private async System.Threading.Tasks.Task NewOrder()
        {
            var itemsToDelete = OrderItems.ToList();
            foreach (var item in itemsToDelete)
            {
                await _deleteCommand.Execute(item.Id);
            }
            OrderItems.Clear();
            Quantity = "";
            PricePerItem = "";
            OnPropertyChanged(nameof(TotalAmount));
        }

        private bool CanPay()
        {
            return OrderItems.Any();
        }

        private void Pay()
        {
            MessageBox.Show($"Payment of ₱{TotalAmount:N2} received!\nThank you!", "Payment", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanCancel()
        {
            return OrderItems.Any();
        }

        private async void Cancel()
        {
            var result = MessageBox.Show("Are you sure you want to cancel this order?", "Cancel Order", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var itemsToDelete = OrderItems.ToList();
                foreach (var item in itemsToDelete)
                {
                    await _deleteCommand.Execute(item.Id);
                }
                OrderItems.Clear();
                ClearForm();
                OnPropertyChanged(nameof(TotalAmount));
                MessageBox.Show("Order cancelled successfully!", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
