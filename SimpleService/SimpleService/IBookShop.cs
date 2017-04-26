using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SimpleService
{
    [ServiceContract]
    interface IBookShop
    {
        [OperationContract]
        List<Book> GetBooks();
        [OperationContract]
        List<Book> GetBookByID(string id);
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
