
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BooksService
{
    [ServiceContract]
    public interface IBooks
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
    }
}
