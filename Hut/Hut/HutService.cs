using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hut
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class HutService : IHutService,IDisposable
    {
        readonly HutContext _Context = new HutContext();
        public void Dispose()
        {
            
        }

        public List<Customer> GetCustomers()
        {
            return _Context.Customers.ToList();
        }

        public List<Product> GetProducts()
        {
            return _Context.Products.ToList();
        }

        [OperationBehavior(TransactionScopeRequired =true)]
        public void SubmitOrder(Order order)
        {
            _Context.Orders.Add(order);
            _Context.SaveChanges();
        }
    }
}
