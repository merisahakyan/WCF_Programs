using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Composite.Presentation.Commands;
using MyHutClient.HutServices;
using System.Collections.ObjectModel;


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
            var SubitOrderComand = new DelegateComand(OnSubmitOrder);
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
