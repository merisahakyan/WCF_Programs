using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BookService
{
    [ServiceContract]
    public interface IOnCard
    {
        [OperationContract]
        List<Book> GetOnCardBooks();

        [OperationContract]
        Book GetOnCardBookByID(string id);

        [OperationContract]
        void RemoveFromCard(string id);

        [OperationContract]
        void AddToCard(string id);


    }
}
