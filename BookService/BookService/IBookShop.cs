using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BookService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    interface IBookShop
    {
        [OperationContract]
        List<Book> GetBooks();
        [OperationContract]
        Book GetBookByID(string id);
        [OperationContract]
        List<Book> GetBooksByAuthor(string author);
        [OperationContract]
        List<Book> GetBooksByName(string name);
        [OperationContract]
        List<Book> GetBooksByGenre(string genre);
        [OperationContract]
        List<Book> GetBooksByPrice(string price);
        [OperationContract]
        List<Book> Search(string value);
        [OperationContract]
        List<OnCard> GetOnCardBooks();

        [OperationContract]
        OnCard GetOnCardBookByID(string id);

        [OperationContract]
        void RemoveFromCard(string id);

        [OperationContract]
        void AddToCard(string id);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "BookService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
