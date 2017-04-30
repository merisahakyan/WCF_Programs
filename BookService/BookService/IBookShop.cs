using System.Collections.Generic;
using System.ServiceModel;

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
    }
}
