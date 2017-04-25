using System;
using Microsoft.Practices.Prism.Mvvm;
using MyHutClient.HutServices;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;

namespace MyHutClient
{
    public class MainWindowViewModel:BindableBase
    {
        private ObservableCollection<Product> _Products;
        private ObservableCollection<Customer> _Customers;
        private ObservableCollection<Hut.OrderItem> _Items=new ObservableCollection<Hut.OrderItem>();
        private Order _CurrentOrder = new Order();

        public MainWindowViewModel()
        {
            _CurrentOrder.OrderDate = DateTime.Now.ToString();
            _CurrentOrder.OrderStatusID = 1;
            SubitOrderComand = new DelegateCommand(OnSubmitOrder);
            AddOrderItemCommand = new Microsoft.Practices.Composite.Presentation.Commands.DelegateCommand<Product>(OnAddItem);
            if(!DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                LoadProductsandCustoers();
            }

        }

        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set { SetProperty(ref _Products, value); }
        }
        public ObservableCollection<Customer> Customers
        {
            get { return _Customers; }
            set { SetProperty(ref _Customers, value); }
        }

        public ObservableCollection<Hut.OrderItem> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        public Order CurrentOrder
        {
            get { return _CurrentOrder; }
            set { SetProperty(ref _CurrentOrder, value); }
        }
        public DelegateCommand SubitOrderComand { get; private set; }
        public Microsoft.Practices.Composite.Presentation.Commands.DelegateCommand<Product> AddOrderItemCommand { get; private set; }

        private void OnAddItem(Order order)
        {
            
        }
        private async void LoadProductsandCustoers()
        {
            HutServiceClient proxy = new HutServiceClient();
            Products = await proxy.GetProductsAsync();
            Customers = await proxy.GetCustomersAsync();
            proxy.Close();
        }
        private void OnSubmitOrder()
        {

        }
    }
}
