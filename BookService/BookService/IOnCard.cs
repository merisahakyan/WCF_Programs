using System.Collections.Generic;
using System.ServiceModel;

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
